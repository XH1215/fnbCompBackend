using orderAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    /// <summary>
    /// Defines persistence operations for queue entries
    /// </summary>
    public interface IQueueRepository
    {
        Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests);
        Task<Queue?> UpdateQueueAsync(int queueId, string? specialRequests);
        Task<bool> CancelQueueAsync(int queueId);
        Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId);
        Task<bool> NotifyNextCustomerAsync(int outletId);
    }
}