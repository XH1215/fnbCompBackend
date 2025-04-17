using System;

namespace orderAPI.DTO
{
    public class BannedCustomerDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime BannedAt { get; set; }
    }
} 