using orderAPI.Models;
using ReservationOrder = orderAPI.Models.Reservation;

namespace orderAPI.Results.Reservation
{
    public class CreateReservationResult : BaseResult<ReservationOrder>
    {
        public CreateReservationResult(bool success, string message, ReservationOrder data)
            : base(success, message, data)
        {
        }

        public CreateReservationResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
