using orderAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Repositories
{
    public interface INotificationRepository
    {
        Task<NotificationLog> CreateLogAsync(NotificationLog log);
        Task<List<NotificationLog>> GetLogsAsync(string? phone = null, string? type = null);
        Task<NotificationLog?> GetLogByIdAsync(int id);
        Task<bool> UpdateLogAsync(NotificationLog log);
    }
}
