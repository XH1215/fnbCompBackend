namespace orderAPI.Requests.Staff
{
    public class UpdateStaffRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? OutletId { get; set; }
        public bool IsActive { get; set; }
    }
} 