//using orderAPI.Data;
//using orderAPI.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace orderAPI.Services
//{
//    public class QueueService : IQueueService
//    {
//        private readonly AppDbContext _context;

//        public QueueService(AppDbContext context)
//        {
//            _context = context;
//        }

//        /// <summary>
//        /// 加入队列时使用悲观锁锁定相同 outletId 下“Waiting”的所有记录，
//        /// 防止并发插入导致队列位置计算出错。
//        /// </summary>
//        public async Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests)
//        {
//            // 使用事务包装整个操作
//            using var transaction = await _context.Database.BeginTransactionAsync();

//            // 悲观锁定当前 outlet 下所有等待中的队列记录
//            var waitingQueues = await _context.Queue
//                .FromSqlInterpolated($@"
//                    SELECT * FROM Queue 
//                    WHERE outlet_id = {outletId} AND status = 'Waiting'
//                    FOR UPDATE")
//                .ToListAsync();

//            int position = waitingQueues.Count + 1;

//            var queue = new Queue
//            {
//                contact_number = contactNumber,
//                outlet_id = outletId,
//                number_of_guests = numberOfGuests,
//                special_requests = specialRequests,
//                status = "Waiting",
//                queue_position = position,
//                created_at = DateTime.UtcNow
//            };

//            _context.Queue.Add(queue);
//            await _context.SaveChangesAsync();

//            // 提交事务后释放锁
//            await transaction.CommitAsync();

//            return queue;
//        }

//        /// <summary>
//        /// 查询队列状态，不需要悲观锁，只做普通查询
//        /// </summary>
//        public async Task<Queue?> GetQueueStatusAsync(string contactNumber, int outletId)
//        {
//            return await _context.Queue
//                .Where(q => q.contact_number == contactNumber && q.outlet_id == outletId && q.status == "Waiting")
//                .OrderBy(q => q.created_at)
//                .FirstOrDefaultAsync();
//        }

//        /// <summary>
//        /// 取消队列，仅更新单条记录，不使用悲观锁
//        /// </summary>
//        public async Task<bool> CancelQueueAsync(int queueId)
//        {
//            var queue = await _context.Queue.FindAsync(queueId);
//            if (queue == null) return false;

//            queue.status = "Cancelled";
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        /// <summary>
//        /// 获取所有等待中的队列记录，不修改数据，不需要使用锁
//        /// </summary>
//        public async Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId)
//        {
//            return await _context.Queue
//                .Where(q => q.outlet_id == outletId && q.status == "Waiting")
//                .OrderBy(q => q.queue_position)
//                .ToListAsync();
//        }

//        /// <summary>
//        /// 通知下一个客户时，需要锁定对应 outlet 下的队列记录，防止并发通知同一记录。
//        /// </summary>
//        public async Task<bool> NotifyNextCustomerAsync(int outletId)
//        {
//            using var transaction = await _context.Database.BeginTransactionAsync();

//            // 锁定 outletId 下最前面的等待记录
//            var next = await _context.Queue
//                .FromSqlInterpolated($@"
//                    SELECT * FROM Queue 
//                    WHERE outlet_id = {outletId} AND status = 'Waiting'
//                    ORDER BY queue_position 
//                    LIMIT 1
//                    FOR UPDATE")
//                .FirstOrDefaultAsync();

//            if (next == null)
//            {
//                await transaction.RollbackAsync();
//                return false;
//            }

//            next.status = "Notified";
//            next.notified_at = DateTime.UtcNow;
//            await _context.SaveChangesAsync();

//            await transaction.CommitAsync();

//            return true;
//        }
//    }
//}
