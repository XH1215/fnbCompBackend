namespace orderAPI.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; } // No longer using foreign key
        public int OutletId { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
        public string Status { get; set; } // Enum: Waiting, Notified, Served, Cancelled
        public int? QueuePosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public DateTime? ServedAt { get; set; }

        public Outlet Outlet { get; set; }
    }

}

