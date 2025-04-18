using orderAPI.DTO;

namespace orderAPI.Results.Notification
{
    public class GetNotificationLogsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<NotificationLogDTO>? Logs { get; set; }

        public GetNotificationLogsResult()
        {
        }

        public GetNotificationLogsResult(bool success, string message, List<NotificationLogDTO>? logs = null)
        {
            Success = success;
            Message = message;
            Logs = logs;
        }
    }
}