using orderAPI.Requests.Reservation;
using orderAPI.Results.Reservation;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public interface IReservationService
    {
        Task<CreateReservationResult> CreateReservationAsync(ReservationCreateRequest req);
        Task<GetReservationsResult> GetReservationsAsync(ReservationGetRequest req);
        Task<UpdateReservationResult> UpdateReservationAsync(ReservationUpdateRequest req);
        Task<CancelReservationResult> CancelReservationAsync(ReservationCancelRequest req);
    }
}
