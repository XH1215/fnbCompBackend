namespace orderAPI.Requests.Queue
{
    public class JoinQueueRequest
    {
        public string ContactNumber { get; set; } = string.Empty;
        public int OutletId { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
    }
}
