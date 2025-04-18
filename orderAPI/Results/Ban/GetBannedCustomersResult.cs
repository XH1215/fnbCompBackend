using orderAPI.DTO;

namespace orderAPI.Results.Ban
{
    public class GetBannedCustomersResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<BannedCustomerDTO>? BannedCustomers { get; set; }

        public GetBannedCustomersResult()
        {
        }

        public GetBannedCustomersResult(bool success, string message, IEnumerable<BannedCustomerDTO>? bannedCustomers = null)
        {
            Success = success;
            Message = message;
            BannedCustomers = bannedCustomers;
        }
    }
}