using orderAPI.Models;

namespace orderAPI.Repositories
{
    public interface IBanRepository
    {
        Task<BannedCustomer> BanCustomerAsync(BannedCustomer bannedCustomer);
        Task<bool> UnbanCustomerAsync(int id);
        Task<bool> IsBannedAsync(string phone);
        Task<List<BannedCustomer>> GetAllBannedCustomersAsync();
        Task<BannedCustomer?> GetBannedCustomerByIdAsync(int id);
    }
}
