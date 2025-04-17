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

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> SendWhatsAppMessageAsync(string phone, string message, string type)
        {
            bool isSuccessful = false;
            string status = "Failed";

            try
            {
                // Here you would implement the actual WhatsApp API call
                // For now, let's simulate a successful message
                isSuccessful = true;
                status = "Sent";

                // In a real implementation, you would make a call to the WhatsApp API
                // If it fails, set status to "Failed"
            }
            catch (Exception)
            {
                isSuccessful = false;
                status = "Failed";
            }

            // Log the notification attempt regardless of success/failure
            var log = new NotificationLog
            {
                ContactNumber = phone,
                Message = message,
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

        public async Task<bool> ResendFailedNotificationAsync(int logId)
        {
            var log = await _notificationRepository.GetLogByIdAsync(logId);
            
            if (log == null || log.Status == "Sent")
                return false;

            // Attempt to resend the message
            bool isSuccessful = false;
            string status = "Failed";

            try
            {
                // Here you would implement the actual resend logic
                // For now, let's simulate a successful message
                isSuccessful = true;
                status = "Sent";
            }
            catch (Exception)
            {
                isSuccessful = false;
                status = "Failed";
            }

            // Create a new log entry for the resend attempt
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
                // Update the status of the original log
                log.Status = "Failed";
                await _notificationRepository.UpdateLogAsync(log);
            }

            return isSuccessful;
        }
    }
}
