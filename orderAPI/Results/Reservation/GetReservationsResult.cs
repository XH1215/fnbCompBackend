using ReservationOrder = orderAPI.Models.Reservation;

namespace orderAPI.Results.Reservation
{
    public class GetReservationsResult : BaseResult<List<ReservationOrder>>
    {
        public GetReservationsResult(bool success, string message, List<ReservationOrder> data)
            : base(success, message, data)
        {
        }

        public GetReservationsResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
