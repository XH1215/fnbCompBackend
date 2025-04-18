namespace orderAPI.Requests.Staff
{
    public class CreateStaffRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? OutletId { get; set; }
    }
}