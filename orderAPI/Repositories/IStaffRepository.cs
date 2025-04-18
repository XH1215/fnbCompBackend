using orderAPI.Models;

namespace orderAPI.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff> CreateStaffAsync(Staff newStaff);
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<List<Staff>> GetAllStaffAsync(string? role = null, int? outletId = null);
        Task<bool> UpdateStaffAsync(Staff updatedStaff);
        Task<bool> DeactivateStaffAsync(int staffId);

        Task<bool> EmailExistsAsync(string email);
        Task<Staff?> LoginStaffAsync(string email);

        Task<Staff?> GetStaffByEmailAsync(string email);
    }

}
