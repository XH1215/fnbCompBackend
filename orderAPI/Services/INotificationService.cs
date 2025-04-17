using orderAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public interface INotificationService
    {
        Task<bool> SendWhatsAppMessageAsync(string phone, string message, string type);
        Task<List<NotificationLogDTO>> GetLogsAsync(string? phone = null, string? type = null);
        Task<bool> ResendFailedNotificationAsync(int logId);
    }
}
