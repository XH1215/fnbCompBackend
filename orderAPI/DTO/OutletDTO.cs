using orderAPI.Models;

namespace orderAPI.DTO
{
    public class OutletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? OperatingHours { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 