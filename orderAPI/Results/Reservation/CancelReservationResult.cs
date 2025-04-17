namespace OrderAPI.Results.Reservation
{
    public class CancelReservationResult : BaseResult<bool>
    {
        public CancelReservationResult(bool success, string message, bool data)
            : base(success, message, data)
        {
        }

        public CancelReservationResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
