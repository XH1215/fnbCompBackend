// Repositories/IReservationRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using orderAPI.Models;

namespace orderAPI.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservationAsync(
            string contactNumber,
            int outletId,
            DateTime date,
            TimeSpan time,
            int guests,
            string? specialRequests);

        Task<List<Reservation>> GetReservationsByContactAsync(string contactNumber);

        Task<Reservation?> UpdateReservationAsync(
            int reservationId,
            DateTime date,
            TimeSpan time,
            int guests,
            string? specialRequests);

        Task<bool> CancelReservationAsync(int reservationId);
    }
}
