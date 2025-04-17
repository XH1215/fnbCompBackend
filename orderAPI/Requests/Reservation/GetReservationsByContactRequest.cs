namespace orderAPI.Request.Reservation
{
    public class GetReservationsByContactRequest
    {
        /// <summary>
        /// 用户联系方式
        /// </summary>
        public string ContactNumber { get; set; } = string.Empty;
    }
}
