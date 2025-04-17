// Repositories/IQueueRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using orderAPI.Models;

namespace orderAPI.Repositories
{
    public interface IQueueRepository
    {
        Task<Queue> JoinQueueAsync(string contactNumber, int outletId, int numberOfGuests, string? specialRequests);
        Task<Queue?> UpdateQueueAsync(int queueId, string? specialRequests);
        Task<bool> CancelQueueAsync(int queueId);
        Task<List<Queue>> GetAllWaitingQueueEntriesAsync(int outletId);
        Task<bool> NotifyNextCustomerAsync(int outletId);
    }
}
