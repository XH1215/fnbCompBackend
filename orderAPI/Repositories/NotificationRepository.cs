using Microsoft.EntityFrameworkCore;
using orderAPI.Data;
using orderAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationLog> CreateLogAsync(NotificationLog log)
        {
            _context.NotificationLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<List<NotificationLog>> GetLogsAsync(string? phone = null, string? type = null)
        {
            var query = _context.NotificationLogs.AsQueryable();

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(n => n.ContactNumber == phone);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(n => n.Type == type);

            return await query.OrderByDescending(n => n.SentAt).ToListAsync();
        }

        public async Task<NotificationLog?> GetLogByIdAsync(int id)
        {
            return await _context.NotificationLogs.FindAsync(id);
        }

        public async Task<bool> UpdateLogAsync(NotificationLog log)
        {
            _context.NotificationLogs.Update(log);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
