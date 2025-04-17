
using orderAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    public interface IOutletRepository
    {
        Task<Outlet> CreateOutletAsync(Outlet newOutlet);
        Task<Outlet?> GetOutletByIdAsync(int id);
        Task<List<Outlet>> GetAllOutletAsync();
        Task<bool> UpdateOutletAsync(Outlet updatedOutlet);
        Task<bool> DeleteOutletAsync(int outletId);


    }

}
