namespace orderAPI.Models
{
    public class BannedCustomer
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; }
        public string Reason { get; set; }
        public DateTime BannedAt { get; set; }
    }

}
