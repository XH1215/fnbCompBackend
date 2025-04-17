namespace orderAPI.Results.Reservation
{
	public class UpdateReservationResult : BaseResult<bool>
	{
		public UpdateReservationResult(bool success, string message, bool data)
			: base(success, message, data)
		{
		}

		public UpdateReservationResult(bool success, string message)
			: base(success, message)
		{
		}
	}
}
