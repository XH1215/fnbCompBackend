namespace orderAPI.Requests.Reservation
{
    public class UpdateReservationRequest
    {
        /// <summary>
        /// 预订的唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 预定日期（仅日期部分）
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// 预定时间（仅时间部分）
        /// </summary>
        public TimeSpan ReservationTime { get; set; }

        /// <summary>
        /// 预定人数
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// 特殊要求（可选）
        /// </summary>
        public string? SpecialRequests { get; set; }
    }
}
