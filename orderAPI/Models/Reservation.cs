namespace orderAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; } // No longer using foreign key
        public int OutletId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
        public string Status { get; set; } // Enum: Pending, Confirmed, Cancelled, Completed
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Outlet Outlet { get; set; }
    }

}

