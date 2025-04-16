namespace orderAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Queue> Queues { get; set; }
    }

}
