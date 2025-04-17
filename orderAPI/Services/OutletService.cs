using Microsoft.EntityFrameworkCore;
using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using orderAPI.Repositories;
using System.Data;


namespace orderAPI.Services
{
    public class OutletService : IOutletService
    {

        private readonly IOutletRepository _outletRepository;

        public OutletService(IOutletRepository outletRepository)
        {
            _outletRepository = outletRepository;
        }


        public async Task<OutletDTO> CreateOutletAsync(string name, string location, string OperatingHours, int Capacity)
        {

            var outlet = new Outlet
            {
                Name = name,
                Location = location,
                OperatingHours = OperatingHours,
                Capacity = Capacity,
                CreatedAt = DateTime.UtcNow
            };

            await _outletRepository.CreateOutletAsync(outlet);
            return outlet.ToDto();
        }

        public async Task<OutletDTO?> GetOutletByIdAsync(int outletId)
        {
            var outlet = await _outletRepository.GetOutletByIdAsync(outletId);
            return outlet?.ToDto();
        }

        public async Task<IEnumerable<OutletDTO>> GetAllOutletAsync()
        {
            var outlets = await _outletRepository.GetAllOutletAsync();
            return outlets.ToDtoList();
        }

        public async Task<bool> UpdateOutletAsync(Outlet updatedOutlet)
        {
            var existingOutlet = await _outletRepository.GetOutletByIdAsync(updatedOutlet.Id);
            if (existingOutlet == null)
                return false;

            existingOutlet.Name = updatedOutlet.Name;
            existingOutlet.Location = updatedOutlet.Location;
            existingOutlet.OperatingHours = updatedOutlet.OperatingHours;
            existingOutlet.Capacity = updatedOutlet.Capacity;


            return await _outletRepository.UpdateOutletAsync(updatedOutlet);
        }

        public async Task<bool> DeleteOutletAsync(int outletId)
        {

            return await _outletRepository.DeleteOutletAsync(outletId);
        }
    }



}

