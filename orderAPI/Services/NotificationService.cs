using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using orderAPI.Repositories;

namespace orderAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IWhatsAppService _whatsAppService;

        public NotificationService(INotificationRepository notificationRepository, IWhatsAppService whatsAppService)
        {
            _notificationRepository = notificationRepository;
            _whatsAppService = whatsAppService;
        }

        public async Task<bool> SendWhatsAppMessageAsync(string phone, string customerName, int queue, string template, string type)
        {
            bool isSuccessful = await _whatsAppService.SendTemplateMessageAsync(phone, customerName, queue);
            string status = isSuccessful ? "Sent" : "Failed";

            var log = new NotificationLog
            {
                ContactNumber = phone,
                Message = template,
                Type = type,
                Status = status,
                SentAt = DateTime.UtcNow
            };

            await _notificationRepository.CreateLogAsync(log);
            return isSuccessful;
        }

        public async Task<List<NotificationLogDTO>> GetLogsAsync(string? phone = null, string? type = null)
        {
            var logs = await _notificationRepository.GetLogsAsync(phone, type);
            return logs.ToDtoList();
        }
    }
}
