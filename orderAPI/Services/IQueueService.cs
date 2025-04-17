using orderAPI.Request.Queue;
using orderAPI.Requests.Queue;
using orderAPI.Results.Queue;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public interface IQueueService
    {
        Task<JoinQueueResult> JoinQueueAsync(QueueCreateRequest req);
        Task<GetQueueStatusResult> GetQueueStatusAsync(QueueStatusRequest req);
        Task<CancelQueueResult> CancelQueueAsync(QueueCancelRequest req);
        Task<GetAllWaitingQueueEntriesResult> GetAllWaitingQueueEntriesAsync(AllWaitingQueueRequest req);
        Task<NotifyNextCustomerResult> NotifyNextCustomerAsync(NotifyNextCustomerRequest req);
    }
}
