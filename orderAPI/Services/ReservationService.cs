using orderAPI.Data;
using orderAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 创建预订，并在同一 outlet 下进行悲观锁定，防止并发问题。
        /// </summary>
        public async Task<Reservation> CreateReservationAsync(string contactNumber, int outletId, DateTime date, TimeSpan time, int guests, string? specialRequests)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定当前 outlet 下所有预订记录（如果需要根据具体情况做冲突检测，可增加相应条件）
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
        /// 根据联系人查询预订记录，不需要加锁
        /// </summary>
        public async Task<List<Reservation>> GetReservationsByContactAsync(string contactNumber)
        {
            return await _context.Reservations
                .Where(r => r.contact_number == contactNumber)
                .OrderByDescending(r => r.reservation_date)
                .ToListAsync();
        }

        /// <summary>
        /// 更新预订记录，先通过悲观锁获取该记录，确保数据在并发环境下修改时的正确性
        /// </summary>
        public async Task<bool> UpdateReservationAsync(Reservation updated)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定当前要更新的预订记录
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
        /// 取消预订，使用悲观锁锁定该记录，防止多个并发取消或更新导致数据异常
        /// </summary>
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定对应的预订记录
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
