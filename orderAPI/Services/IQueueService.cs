using System.Collections.Generic;
using System.Threading.Tasks;
using orderAPI.Models;

namespace orderAPI.Services
{
    public interface IQueueService
    {
        Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests);
        Task<Queue?> GetQueueStatusAsync(string contactNumber, int outletId);
        Task<bool> CancelQueueAsync(int queueId);
        Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId);
        Task<bool> NotifyNextCustomerAsync(int outletId);
    }
}
