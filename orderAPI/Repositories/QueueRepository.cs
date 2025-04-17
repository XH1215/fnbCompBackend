using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;
using System;
using System.Collections.Generic;
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

        public async Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests)
        {
            // 使用 RepeatableRead 级别的事务进行悲观锁定
            await using var tx = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            // 锁定当前 outlet 下所有 Waiting 的记录
            var waiting = await _context.Queue
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

            _context.Queue.Add(entry);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return entry;
        }

        public async Task<Queue?> UpdateQueueAsync(int queueId, string? specialRequests)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            // 锁定指定队列记录
            var entry = await _context.Queue
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
            await using var tx = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            var entry = await _context.Queue
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
            // 纯查询，无需锁
            return await _context.Queue
                .Where(q => q.OutletId == outletId && q.Status == "Waiting")
                .OrderBy(q => q.QueuePosition)
                .ToListAsync();
        }

        public async Task<bool> NotifyNextCustomerAsync(int outletId)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);

            // 锁定最先加入的 Waiting 记录
            var next = await _context.Queue
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
