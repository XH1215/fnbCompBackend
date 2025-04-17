using Microsoft.EntityFrameworkCore;
using orderAPI.Models;

namespace orderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<BannedCustomer> BannedCustomers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
    }
}
