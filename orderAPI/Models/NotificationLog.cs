using System;

namespace orderAPI.Models
{
    public class NotificationLog
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; }
        public string Type { get; set; } // Enum: ReservationConfirmation, QueueNotification, General
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public string Status { get; set; } // Enum: Sent, Failed
    }
}
