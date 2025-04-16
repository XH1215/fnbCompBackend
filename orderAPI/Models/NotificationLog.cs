namespace orderAPI.Models
{
    public class NotificationLog
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Type { get; set; } // ReservationConfirmation, QueueNotification, General
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public string Status { get; set; } // Sent, Failed

        // Navigation Property
        public Customer Customer { get; set; }
    }

}
