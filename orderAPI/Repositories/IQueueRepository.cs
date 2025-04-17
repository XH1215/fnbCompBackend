using orderAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    public interface IQueueRepository
    {
        Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests);
        Task<Queue?> GetQueueStatusAsync(string contactNumber, int outletId);
        Task<bool> CancelQueueAsync(int queueId);
        Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId);
        Task<bool> NotifyNextCustomerAsync(int outletId);
    }
}
