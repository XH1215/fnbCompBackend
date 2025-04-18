using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using orderAPI.Repositories;

namespace orderAPI.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


        public async Task<StaffDTO> CreateStaffAsync(string name, string email, string password, string role, int? outletId)
        {
            var emailExists = await _staffRepository.EmailExistsAsync(email);
            if (emailExists)
                throw new Exception("Email already exists");

            var staff = new Staff
            {
                Name = name,
                Email = email,
                PasswordHash = HashPassword(password),
                Role = role,
                OutletId = outletId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _staffRepository.CreateStaffAsync(staff);
            return staff.ToDto();
        }

        public async Task<IEnumerable<StaffDTO>> GetAllStaffAsync(string? role = null, int? outletId = null)
        {
            var staffs = await _staffRepository.GetAllStaffAsync(role, outletId);
            return staffs.Select(s => s.ToDto());
        }

        public async Task<StaffDTO?> GetStaffByEmailAsync(string email)
        {
            var staff = await _staffRepository.GetStaffByEmailAsync(email);
            return staff?.ToDto();
        }

        public async Task<StaffDTO?> GetStaffByIdAsync(int id)
        {
            var staff = await _staffRepository.GetStaffByIdAsync(id);
            return staff?.ToDto();
        }

        public async Task<bool> UpdateStaffAsync(Staff updatedStaff)
        {
            var existingStaff = await _staffRepository.GetStaffByIdAsync(updatedStaff.Id);
            if (existingStaff == null)
                return false;

            existingStaff.Name = updatedStaff.Name;
            existingStaff.Email = updatedStaff.Email;
            existingStaff.Role = updatedStaff.Role;
            existingStaff.OutletId = updatedStaff.OutletId;
            existingStaff.IsActive = updatedStaff.IsActive;

            return await _staffRepository.UpdateStaffAsync(existingStaff);
        }

        public async Task<bool> DeactivateStaffAsync(int id)
        {
            return await _staffRepository.DeactivateStaffAsync(id);
        }

        public async Task<bool> VerifyPasswordAsync(string email, string password)
        {
            var staff = await _staffRepository.LoginStaffAsync(email);
            if (staff == null)
                return false;

            return VerifyPassword(password, staff.PasswordHash);
        }


        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            string newHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hash == newHash;
        }

    }
}
