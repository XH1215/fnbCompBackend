namespace orderAPI.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public int ContactNumber { get; set; }
        public int OutletId { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; }
        public string Status { get; set; } // Waiting, Notified, Served, Cancelled
        public int QueuePosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public DateTime? ServedAt { get; set; }

        // Navigation Properties

        public Outlet Outlet { get; set; }
    }
}

