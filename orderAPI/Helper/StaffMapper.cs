using orderAPI.DTO;
using orderAPI.Models;

namespace orderAPI.Helper
{
    public static class StaffMapper
    {
        public static StaffDTO ToDto(this Staff staff)
        {
            return new StaffDTO
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Role = staff.Role,
                OutletId = staff.OutletId,
                IsActive = staff.IsActive,
                CreatedAt = staff.CreatedAt
            };
        }
    }
}
