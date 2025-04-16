namespace orderAPI.Models
{
    public class Reservation
    {
  
            public int Id { get; set; }

            public int ContactNumber { get; set; }

            public int OutletId { get; set; }
            public Outlet Outlet { get; set; }

            public DateTime ReservationDate { get; set; }
            public TimeSpan ReservationTime { get; set; }

            public int NumberOfGuests { get; set; }
            public string? SpecialRequests { get; set; }

        // Pending, Confirmed, Cancelled, Completed
        public string Status { get; set; } = "Pending"; 
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        }
    }

