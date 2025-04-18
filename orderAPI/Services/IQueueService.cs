using orderAPI.Requests.Queue;
using orderAPI.Results.Queue;

namespace orderAPI.Services
{
    /// <summary>
    /// Defines business operations for queue management
    /// </summary>
    public interface IQueueService
    {
        Task<JoinQueueResult> JoinQueueAsync(JoinQueueRequest req);
        Task<UpdateQueueResult> UpdateQueueAsync(UpdateQueueRequest req);
        Task<CancelQueueResult> CancelQueueAsync(QueueCancelRequest req);
        Task<GetAllWaitingQueueEntriesResult> GetAllWaitingQueueEntriesAsync(AllWaitingQueueRequest req);
        Task<NotifyNextCustomerResult> NotifyNextCustomerAsync(NotifyNextCustomerRequest req);
    }
}