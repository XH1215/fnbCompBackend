using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using orderAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> SendWhatsAppMessageAsync(string phone, string template, string type)
        {
            bool isSuccessful = await _whatsAppService.SendTemplateMessageAsync(phone, template);
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

        public async Task<bool> ResendFailedNotificationAsync(int logId)
        {
            var log = await _notificationRepository.GetLogByIdAsync(logId);
            if (log == null || log.Status == "Sent") return false;

            bool isSuccessful = await _whatsAppService.SendTemplateMessageAsync(log.ContactNumber, log.Message);
            if (isSuccessful)
            {
                var newLog = new NotificationLog
                {
                    ContactNumber = log.ContactNumber,
                    Message = log.Message,
                    Type = log.Type,
                    Status = "Sent",
                    SentAt = DateTime.UtcNow
                };
                await _notificationRepository.CreateLogAsync(newLog);
            }
            else
            {
                log.Status = "Failed";
                await _notificationRepository.UpdateLogAsync(log);
            }

            return isSuccessful;
        }


        public async Task<List<NotificationLogDTO>> GetLogsAsync(string? phone = null, string? type = null)
        {
            var logs = await _notificationRepository.GetLogsAsync(phone, type);
            return logs.ToDtoList();
        }
    }
}
