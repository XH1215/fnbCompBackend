using orderAPI.DTO;

namespace orderAPI.Services
{
    public interface IBanService
    {
        Task<BannedCustomerDTO> BanCustomerAsync(string phone, string reason);
        Task<bool> UnbanCustomerAsync(int id);
        Task<bool> IsBannedAsync(string phone);
        Task<IEnumerable<BannedCustomerDTO>> GetAllBannedCustomersAsync();
        Task<BannedCustomerDTO?> GetBannedCustomerByIdAsync(int id);
    }
}
