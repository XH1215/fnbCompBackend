﻿namespace orderAPI.Models
{
    public class Outlet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? OperatingHours { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Staff> Staffs { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Queue> Queues { get; set; }
    }


}
