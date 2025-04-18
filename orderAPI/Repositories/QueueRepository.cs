using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{

    public class QueueRepository : IQueueRepository
    {
        private readonly AppDbContext _context;

        public QueueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Queue> JoinQueueAsync(
            string contactNumber,
            int outletId,
            int numberOfGuests,
            string? specialRequests)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            var waiting = await _context.Queues
                .FromSqlInterpolated($@"
                    SELECT * FROM `Queue`
                    WHERE outlet_id = {outletId}
                      AND status = 'Waiting'
                    FOR UPDATE")
                .ToListAsync();

            var position = waiting.Count + 1;
            var entry = new Queue
            {
                ContactNumber = contactNumber,
                OutletId = outletId,
                NumberOfGuests = numberOfGuests,
                SpecialRequests = specialRequests,
                Status = "Waiting",
                QueuePosition = position,
                CreatedAt = DateTime.UtcNow
            };

            _context.Queues.Add(entry);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return entry;
        }

        public async Task<Queue?> UpdateQueueAsync(int queueId, string? specialRequests)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            var entry = await _context.Queues
                .FromSqlInterpolated($@"
                    SELECT * FROM `Queue`
                    WHERE id = {queueId}
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                await tx.RollbackAsync();
                return null;
            }

            entry.SpecialRequests = specialRequests;
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return entry;
        }
        public async Task<bool> CancelQueueAsync(int queueId)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            var entry = await _context.Queues
                .FromSqlInterpolated($@"
                    SELECT * FROM `Queue`
                    WHERE id = {queueId}
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                await tx.RollbackAsync();
                return false;
            }

            entry.Status = "Cancelled";
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }

        public async Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId)
        {
            return await _context.Queues
                .Where(q => q.OutletId == outletId && q.Status == "Waiting")
                .OrderBy(q => q.QueuePosition)
                .ToListAsync();
        }

        public async Task<bool> NotifyNextCustomerAsync(int outletId)
        {
            await using var tx = await _context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            var next = await _context.Queues
                .FromSqlInterpolated($@"
                    SELECT * FROM `Queue`
                    WHERE outlet_id = {outletId}
                      AND status = 'Waiting'
                    ORDER BY queue_position
                    LIMIT 1
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (next == null)
            {
                await tx.RollbackAsync();
                return false;
            }

            next.Status = "Notified";
            next.NotifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }
    }
}