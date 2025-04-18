using orderAPI.DTO;
using orderAPI.Models;

namespace orderAPI.Helper
{
    public static class BannedCustomerMapper
    {
        public static BannedCustomerDTO ToDto(this BannedCustomer bannedCustomer)
        {
            return new BannedCustomerDTO
            {
                Id = bannedCustomer.Id,
                Phone = bannedCustomer.ContactNumber,
                Reason = bannedCustomer.Reason,
                BannedAt = bannedCustomer.BannedAt
            };
        }

        public static List<BannedCustomerDTO> ToDtoList(this IEnumerable<BannedCustomer> bannedCustomers)
        {
            return bannedCustomers.Select(b => b.ToDto()).ToList();
        }
    }
}