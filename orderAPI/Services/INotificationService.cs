using orderAPI.DTO;

namespace orderAPI.Services
{
    public interface INotificationService
    {
        Task<bool> SendWhatsAppMessageAsync(string phone, string customerName, int queue, string template, string type);
        Task<List<NotificationLogDTO>> GetLogsAsync(string? phone = null, string? type = null);
    }
}
