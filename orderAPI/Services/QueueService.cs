using orderAPI.Repositories;
using orderAPI.Requests.Queue;
using orderAPI.Results.Queue;

namespace orderAPI.Services
{
    /// <summary>
    /// Implementation of queue operations
    /// </summary>
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _repo;

        public QueueService(IQueueRepository repo)
        {
            _repo = repo;
        }

        public async Task<JoinQueueResult> JoinQueueAsync(JoinQueueRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.ContactNumber))
                return new JoinQueueResult(false, "ContactNumber is required");

            if (req.OutletId <= 0)
                return new JoinQueueResult(false, "OutletId must be greater than zero");

            if (req.NumberOfGuests <= 0)
                return new JoinQueueResult(false, "NumberOfGuests must be at least 1");

            try
            {
                var entry = await _repo.JoinQueueAsync(
                    req.ContactNumber,
                    req.OutletId,
                    req.NumberOfGuests,
                    req.SpecialRequests);

                return new JoinQueueResult(true, "Joined queue successfully", entry);
            }
            catch (Exception ex)
            {
                return new JoinQueueResult(false, $"Failed to join queue: {ex.Message}");
            }
        }

        public async Task<UpdateQueueResult> UpdateQueueAsync(UpdateQueueRequest req)
        {
            if (req.QueueId <= 0)
                return new UpdateQueueResult(false, "QueueId is invalid");

            try
            {
                var entry = await _repo.UpdateQueueAsync(
                    req.QueueId,
                    req.SpecialRequests);

                if (entry == null)
                    return new UpdateQueueResult(false, "Queue entry not found");

                return new UpdateQueueResult(true, "Queue updated successfully", entry);
            }
            catch (Exception ex)
            {
                return new UpdateQueueResult(false, $"Failed to update queue: {ex.Message}");
            }
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
                return new GetAllWaitingQueueEntriesResult(false, "OutletId must be greater than zero");

            var list = await _repo.GetAllWaitingQueueEntriesAsync(req.OutletId);
            return new GetAllWaitingQueueEntriesResult(true, $"Found {list.Count} waiting entries", list);
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