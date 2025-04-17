using orderAPI.Data;
using orderAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 创建预订，同时对 outlet 下的预订记录加锁（悲观锁）以确保数据一致性。
        /// </summary>
        public async Task<Reservation> CreateReservationAsync(string contactNumber, int outletId, DateTime date, TimeSpan time, int guests, string? specialRequests)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定当前 outlet 下的所有预订记录
            var lockedReservations = await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * FROM Reservations 
                    WHERE outlet_id = {outletId}
                    FOR UPDATE")
                .ToListAsync();

            var reservation = new Reservation
            {
                contact_number = contactNumber,
                outlet_id = outletId,
                reservation_date = date.Date,
                reservation_time = time,
                number_of_guests = guests,
                special_requests = specialRequests,
                status = "Pending",
                created_at = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return reservation;
        }

        /// <summary>
        /// 根据联系人查询预订记录，单纯查询无需悲观锁。
        /// </summary>
        public async Task<List<Reservation>> GetReservationsByContactAsync(string contactNumber)
        {
            return await _context.Reservations
                .Where(r => r.contact_number == contactNumber)
                .OrderByDescending(r => r.reservation_date)
                .ToListAsync();
        }

        /// <summary>
        /// 更新预订记录，使用悲观锁对要更新的记录进行锁定，以防并发修改。
        /// </summary>
        public async Task<bool> UpdateReservationAsync(Reservation updated)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定当前要更新的记录
            var original = await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * FROM Reservations 
                    WHERE id = {updated.id}
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (original == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            original.reservation_date = updated.reservation_date;
            original.reservation_time = updated.reservation_time;
            original.number_of_guests = updated.number_of_guests;
            original.special_requests = updated.special_requests;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

        /// <summary>
        /// 取消预订时使用悲观锁锁定记录，确保在并发环境下记录状态唯一。
        /// </summary>
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var res = await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * FROM Reservations 
                    WHERE id = {reservationId} 
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (res == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            res.status = "Cancelled";
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
    }
}
