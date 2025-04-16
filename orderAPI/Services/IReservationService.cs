using System.Collections.Generic;
using System.Threading.Tasks;
using orderAPI.Models;

namespace orderAPI.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(string contactNumber, int outletId, DateTime date, TimeSpan time, int guests, string? specialRequests);
        Task<List<Reservation>> GetReservationsByContactAsync(string contactNumber);
        Task<bool> UpdateReservationAsync(Reservation updated);
        Task<bool> CancelReservationAsync(int reservationId);
    }
}
