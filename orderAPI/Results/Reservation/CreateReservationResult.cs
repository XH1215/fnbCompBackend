using OrderAPI.Models;

namespace OrderAPI.Results.Reservation
{
    public class CreateReservationResult : BaseResult<Reservation>
    {
        public CreateReservationResult(bool success, string message, Reservation data)
            : base(success, message, data)
        {
        }

        public CreateReservationResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
