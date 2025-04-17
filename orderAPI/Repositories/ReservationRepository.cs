// Repositories/ReservationRepository.cs
using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace orderAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservationAsync(
            string contactNumber,
            int outletId,
            DateTime date,
            TimeSpan time,
            int guests,
            string? specialRequests)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(IsolationLevel.RepeatableRead);

            // 悲观锁：锁定该 outlet 下所有记录
            await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * 
                      FROM `Reservations`
                     WHERE outlet_id = {outletId}
                     FOR UPDATE")
                .ToListAsync();

            var reservation = new Reservation
            {
                ContactNumber = contactNumber,
                OutletId = outletId,
                ReservationDate = date.Date,
                ReservationTime = time,
                NumberOfGuests = guests,
                SpecialRequests = specialRequests,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByContactAsync(string contactNumber)
        {
            return await _context.Reservations
                .Where(r => r.ContactNumber == contactNumber)
                .OrderByDescending(r => r.ReservationDate)
                .ToListAsync();
        }

        public async Task<Reservation?> UpdateReservationAsync(
            int reservationId,
            DateTime date,
            TimeSpan time,
            int guests,
            string? specialRequests)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(IsolationLevel.RepeatableRead);

            // 悲观锁：锁定这一条记录
            var original = await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * 
                      FROM `Reservations`
                     WHERE id = {reservationId}
                     FOR UPDATE")
                .FirstOrDefaultAsync();

            if (original == null)
            {
                await tx.RollbackAsync();
                return null;
            }

            original.ReservationDate = date.Date;
            original.ReservationTime = time;
            original.NumberOfGuests = guests;
            original.SpecialRequests = specialRequests;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return original;
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(IsolationLevel.RepeatableRead);

            // 悲观锁：锁定这一条记录
            var res = await _context.Reservations
                .FromSqlInterpolated($@"
                    SELECT * 
                      FROM `Reservations`
                     WHERE id = {reservationId}
                     FOR UPDATE")
                .FirstOrDefaultAsync();

            if (res == null)
            {
                await tx.RollbackAsync();
                return false;
            }

            res.Status = "Cancelled";
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }
    }
}
