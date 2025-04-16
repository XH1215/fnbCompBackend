using orderAPI.Data;
using orderAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// 加入队列，同时使用悲观锁锁定 outletId 下所有等待记录，确保队列位置的计算不会出现并发问题。
        /// </summary>
        public async Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定当前 outlet 下所有等待中的队列记录
            var waitingQueues = await _context.Queue
                .FromSqlInterpolated($@"
                    SELECT * FROM Queue 
                    WHERE outlet_id = {outletId} AND status = 'Waiting'
                    FOR UPDATE")
                .ToListAsync();

            int position = waitingQueues.Count + 1;

            var queue = new Queue
            {
                contact_number = contactNumber,
                outlet_id = outletId,
                number_of_guests = numberOfGuests,
                special_requests = specialRequests,
                status = "Waiting",
                queue_position = position,
                created_at = DateTime.UtcNow
            };

            _context.Queue.Add(queue);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return queue;
        }

        /// <summary>
        /// 获取指定用户在 outlet 下等待中的队列记录，查询不需要加锁。
        /// </summary>
        public async Task<Queue?> GetQueueStatusAsync(string contactNumber, int outletId)
        {
            return await _context.Queue
                .Where(q => q.contact_number == contactNumber && q.outlet_id == outletId && q.status == "Waiting")
                .OrderBy(q => q.created_at)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 取消队列，不需要额外加锁，直接通过主键查找记录并更新状态。
        /// </summary>
        public async Task<bool> CancelQueueAsync(int queueId)
        {
            var queue = await _context.Queue.FindAsync(queueId);
            if (queue == null) return false;

            queue.status = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 获取所有等待中的队列记录（根据 outletId），查询无需加锁。
        /// </summary>
        public async Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId)
        {
            return await _context.Queue
                .Where(q => q.outlet_id == outletId && q.status == "Waiting")
                .OrderBy(q => q.queue_position)
                .ToListAsync();
        }

        /// <summary>
        /// 通知下一个客户时使用悲观锁锁定 outletId 下最前的一条等待记录，防止并发问题。
        /// </summary>
        public async Task<bool> NotifyNextCustomerAsync(int outletId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 锁定 outlet 下最前面的等待记录
            var next = await _context.Queue
                .FromSqlInterpolated($@"
                    SELECT * FROM Queue 
                    WHERE outlet_id = {outletId} AND status = 'Waiting'
                    ORDER BY queue_position 
                    LIMIT 1
                    FOR UPDATE")
                .FirstOrDefaultAsync();

            if (next == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            next.status = "Notified";
            next.notified_at = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
    }
}
