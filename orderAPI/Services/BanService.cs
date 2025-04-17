using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using orderAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public class BanService : IBanService
    {
        private readonly IBanRepository _banRepository;

        public BanService(IBanRepository banRepository)
        {
            _banRepository = banRepository;
        }

        public async Task<BannedCustomerDTO> BanCustomerAsync(string phone, string reason)
        {
            // Check if already banned
            var isAlreadyBanned = await _banRepository.IsBannedAsync(phone);
            if (isAlreadyBanned)
            {
                throw new InvalidOperationException("This phone number is already banned");
            }

            var bannedCustomer = new BannedCustomer
            {
                ContactNumber = phone,
                Reason = reason,
                BannedAt = DateTime.UtcNow
            };

            var result = await _banRepository.BanCustomerAsync(bannedCustomer);
            return result.ToDto();
        }

        public async Task<bool> UnbanCustomerAsync(int id)
        {
            return await _banRepository.UnbanCustomerAsync(id);
        }

        public async Task<bool> IsBannedAsync(string phone)
        {
            return await _banRepository.IsBannedAsync(phone);
        }

        public async Task<IEnumerable<BannedCustomerDTO>> GetAllBannedCustomersAsync()
        {
            var bannedCustomers = await _banRepository.GetAllBannedCustomersAsync();
            return bannedCustomers.ToDtoList();
        }

        public async Task<BannedCustomerDTO?> GetBannedCustomerByIdAsync(int id)
        {
            var bannedCustomer = await _banRepository.GetBannedCustomerByIdAsync(id);
            return bannedCustomer?.ToDto();
        }
    }
}
