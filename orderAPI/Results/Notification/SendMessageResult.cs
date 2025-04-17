namespace orderAPI.Results.Notification
{
    public class SendMessageResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsDelivered { get; set; }

        public SendMessageResult()
        {
        }

        public SendMessageResult(bool success, string message, bool isDelivered)
        {
            Success = success;
            Message = message;
            IsDelivered = isDelivered;
        }
    }
} 