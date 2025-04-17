using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;
using System.Data;

namespace orderAPI.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Staff> CreateStaffAsync(Staff newStaff)
        {
            _context.Staffs.Add(newStaff);
            await _context.SaveChangesAsync();
            return newStaff;
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _context.Staffs.FindAsync(id);
        }

        public async Task<List<Staff>> GetAllStaffAsync(string? role = null, int? outletId = null)
        {
            var query = _context.Staffs.AsQueryable();

            if (!string.IsNullOrEmpty(role))
                query = query.Where(s => s.Role == role);

            if (outletId.HasValue)
                query = query.Where(s => s.OutletId == outletId);

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateStaffAsync(Staff updatedStaff)
        {
            var staff = await _context.Staffs.FindAsync(updatedStaff.Id);
            if (staff == null) return false;

            staff.Name = updatedStaff.Name;
            staff.Email = updatedStaff.Email;
            staff.Role = updatedStaff.Role;
            staff.OutletId = updatedStaff.OutletId;
            staff.IsActive = updatedStaff.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateStaffAsync(int staffId)
        {
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff == null) return false;

            staff.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Staffs.AnyAsync(s => s.Email == email);
        }

        public async Task<Staff?> GetStaffByEmailAsync(string email)
        {
            var query = _context.Staffs.AsQueryable();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(s => s.Email == email);

            return await query.FirstOrDefaultAsync();
        }



        public async Task<Staff?> LoginStaffAsync(string email)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(s => s.Email == email && s.IsActive);
        }




    }

}
