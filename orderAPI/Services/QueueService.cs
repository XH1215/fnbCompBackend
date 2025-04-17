using orderAPI.Models;
using orderAPI.Repositories;
using orderAPI.Requests.Queue;
using orderAPI.Results.Queue;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _repo;

        public QueueService(IQueueRepository repo)
        {
            _repo = repo;
        }

        public async Task<JoinQueueResult> JoinQueueAsync(QueueCreateRequest req)
        {
            // 入参校验
            if (string.IsNullOrWhiteSpace(req.CustomerPhone))
                return new JoinQueueResult(false, "ContactNumber is required", 0, 0);

            if (req.OutletId <= 0)
                return new JoinQueueResult(false, "OutletId must be greater than zero", 0, 0);

            if (req.NumberOfGuests <= 0)
                return new JoinQueueResult(false, "NumberOfGuests must be at least 1", 0, 0);

            try
            {
                var entry = await _repo.JoinQueueAsync(req.CustomerPhone, req.OutletId, req.NumberOfGuests, req.SpecialRequests);
                return new JoinQueueResult(true, "Joined queue successfully", entry.Id, entry.QueuePosition);
            }
            catch (Exception ex)
            {
                return new JoinQueueResult(false, $"Failed to join queue: {ex.Message}", 0, 0);
            }
        }

        public async Task<GetQueueStatusResult> GetQueueStatusAsync(QueueStatusRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.CustomerPhone))
                return new GetQueueStatusResult(false, "ContactNumber is required", null);

            if (req.OutletId <= 0)
                return new GetQueueStatusResult(false, "OutletId must be greater than zero", null);

            var entry = await _repo.GetQueueStatusAsync(req.CustomerPhone, req.OutletId);
            if (entry == null)
                return new GetQueueStatusResult(false, "No waiting queue found", null);

            return new GetQueueStatusResult(true,
                "Queue status retrieved",
                entry);
        }

        public async Task<CancelQueueResult> CancelQueueAsync(QueueCancelRequest req)
        {
            if (req.QueueId <= 0)
                return new CancelQueueResult(false, "QueueId is invalid");

            var ok = await _repo.CancelQueueAsync(req.QueueId);
            return ok
                ? new CancelQueueResult(true, "Queue cancelled successfully")
                : new CancelQueueResult(false, "Queue record not found");
        }

        public async Task<GetAllWaitingQueueEntriesResult> GetAllWaitingQueueEntriesAsync(AllWaitingQueueRequest req)
        {
            if (req.OutletId <= 0)
                return new GetAllWaitingQueueEntriesResult(false, "OutletId must be greater than zero", null);

            var list = await _repo.GetAllWaitingQueueEntriesAsync(req.OutletId);
            return new GetAllWaitingQueueEntriesResult(true,
                $"Found {list.Count} waiting entries",
                list);
        }

        public async Task<NotifyNextCustomerResult> NotifyNextCustomerAsync(NotifyNextCustomerRequest req)
        {
            if (req.OutletId <= 0)
                return new NotifyNextCustomerResult(false, "OutletId must be greater than zero");

            var ok = await _repo.NotifyNextCustomerAsync(req.OutletId);
            return ok
                ? new NotifyNextCustomerResult(true, "Next customer notified")
                : new NotifyNextCustomerResult(false, "No waiting customer to notify");
        }
    }
}
