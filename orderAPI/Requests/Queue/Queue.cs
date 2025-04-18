namespace orderAPI.Requests.Queue
{
    public class JoinQueueRequest
    {
        public string ContactNumber { get; set; } = string.Empty;
        public int OutletId { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
    }

    public class UpdateQueueRequest
    {
        public int QueueId { get; set; }
        public string? SpecialRequests { get; set; }
    }

    public class QueueCancelRequest
    {
        public int QueueId { get; set; }
    }

    public class AllWaitingQueueRequest
    {
        public int OutletId { get; set; }
    }

    public class NotifyNextCustomerRequest
    {
        public int OutletId { get; set; }
    }
}