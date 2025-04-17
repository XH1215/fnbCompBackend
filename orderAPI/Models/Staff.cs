namespace orderAPI.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Enum: Admin, OutletStaff
        public int? OutletId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Outlet? Outlet { get; set; }
    }


}
