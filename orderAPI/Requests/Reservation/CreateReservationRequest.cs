namespace orderAPI.Requests.Reservation
{
    public class CreateReservationRequest
    {
        /// <summary>
        /// 用户联系方式
        /// </summary>
        public string ContactNumber { get; set; } = string.Empty;

        /// <summary>
        /// 门店ID
        /// </summary>
        public int OutletId { get; set; }

        /// <summary>
        /// 预定日期（仅日期部分）
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 预定时间（仅时间部分）
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// 预定人数
        /// </summary>
        public int Guests { get; set; }

        /// <summary>
        /// 特殊要求（可选）
        /// </summary>
        public string? SpecialRequests { get; set; }
    }
}
