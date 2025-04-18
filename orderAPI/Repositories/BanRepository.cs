using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;

namespace orderAPI.Repositories
{
    public class BanRepository : IBanRepository
    {
        private readonly AppDbContext _context;

        public BanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BannedCustomer> BanCustomerAsync(BannedCustomer bannedCustomer)
        {
            _context.BannedCustomers.Add(bannedCustomer);
            await _context.SaveChangesAsync();
            return bannedCustomer;
        }

        public async Task<bool> UnbanCustomerAsync(int id)
        {
            var bannedCustomer = await _context.BannedCustomers.FindAsync(id);
            if (bannedCustomer == null)
                return false;

            _context.BannedCustomers.Remove(bannedCustomer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsBannedAsync(string phone)
        {
            return await _context.BannedCustomers.AnyAsync(b => b.ContactNumber == phone);
        }

        public async Task<List<BannedCustomer>> GetAllBannedCustomersAsync()
        {
            return await _context.BannedCustomers.ToListAsync();
        }

        public async Task<BannedCustomer?> GetBannedCustomerByIdAsync(int id)
        {
            return await _context.BannedCustomers.FindAsync(id);
        }
    }
}
