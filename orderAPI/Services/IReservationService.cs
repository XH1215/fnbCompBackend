using orderAPI.Requests.Reservation;
using orderAPI.Results.Reservation;

namespace orderAPI.Services
{
    public interface IReservationService
    {
        Task<CreateReservationResult> CreateReservationAsync(CreateReservationRequest req);
        Task<GetReservationsResult> GetReservationsAsync(GetReservationsByContactRequest req);
        Task<UpdateReservationResult> UpdateReservationAsync(UpdateReservationRequest req);
        Task<CancelReservationResult> CancelReservationAsync(CancelReservationRequest req);
    }
}
