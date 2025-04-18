using orderAPI.DTO;
using orderAPI.Models;

namespace orderAPI.Services
{
    public interface IStaffService
    {

        Task<StaffDTO> GetStaffByIdAsync(int id);
        Task<StaffDTO> GetStaffByEmailAsync(string email);
        Task<StaffDTO> CreateStaffAsync(string name, string email, string password, string role, int? outletId);
        Task<bool> UpdateStaffAsync(Staff staff);
        Task<bool> DeactivateStaffAsync(int id);

        Task<IEnumerable<StaffDTO>> GetAllStaffAsync(string? role = null, int? outletId = null);

        Task<bool> VerifyPasswordAsync(string email, string password);
    }
}
