namespace orderAPI.DTO
{
    public class NotificationLogDTO
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Enum: ReservationConfirmation, QueueNotification, General
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public string Status { get; set; } = string.Empty; // Enum: Sent, Failed
    }
}