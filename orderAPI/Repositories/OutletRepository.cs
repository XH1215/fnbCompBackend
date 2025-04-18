
using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;

namespace orderAPI.Repositories
{
    public class OutletRepository : IOutletRepository
    {
        private readonly AppDbContext _context;

        public OutletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Outlet> CreateOutletAsync(Outlet newOutlet)
        {
            _context.Outlets.Add(newOutlet);
            await _context.SaveChangesAsync();
            return newOutlet;
        }

        public async Task<Outlet?> GetOutletByIdAsync(int id)
        {
            return await _context.Outlets.FindAsync(id);
        }

        public async Task<List<Outlet>> GetAllOutletAsync()
        {
            var query = _context.Outlets.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateOutletAsync(Outlet updatedOutlet)
        {
            var outlet = await _context.Outlets.FindAsync(updatedOutlet.Id);
            if (outlet == null) return false;

            outlet.Name = updatedOutlet.Name;
            outlet.Location = updatedOutlet.Location;
            outlet.OperatingHours = updatedOutlet.OperatingHours;
            outlet.Capacity = updatedOutlet.Capacity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOutletAsync(int outletId)
        {
            var outlet = await _context.Outlets.FindAsync(outletId);
            if (outlet == null)
                return false;

            _context.Outlets.Remove(outlet);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
