namespace orderAPI.Requests.Queue
{
    public class GetQueueStatusRequest
    {
        public string ContactNumber { get; set; } = string.Empty;
        public int OutletId { get; set; }
    }
}
