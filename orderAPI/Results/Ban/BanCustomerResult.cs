using orderAPI.DTO;

namespace orderAPI.Results.Ban
{
    public class BanCustomerResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BannedCustomerDTO? BannedCustomer { get; set; }

        public BanCustomerResult()
        {
        }

        public BanCustomerResult(bool success, string message, BannedCustomerDTO? bannedCustomer = null)
        {
            Success = success;
            Message = message;
            BannedCustomer = bannedCustomer;
        }
    }
}