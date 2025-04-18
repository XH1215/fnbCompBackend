namespace orderAPI.Results.Notification
{
    public class ResendNotificationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsResent { get; set; }

        public ResendNotificationResult()
        {
        }

        public ResendNotificationResult(bool success, string message, bool isResent)
        {
            Success = success;
            Message = message;
            IsResent = isResent;
        }
    }
}