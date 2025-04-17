using orderAPI.DTO;
using orderAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public interface IOutletService
    {
        Task<OutletDTO> CreateOutletAsync(string name, string location, string OperatingHours, int Capacity);
        Task<OutletDTO?> GetOutletByIdAsync(int outletId);
        Task<IEnumerable<OutletDTO>> GetAllOutletAsync();
        Task<bool> UpdateOutletAsync(Outlet updatedOutlet);
        Task<bool> DeleteOutletAsync(int outletId);
    }
}
