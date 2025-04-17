using orderAPI.DTO;
using orderAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace orderAPI.Helper
{
    public static class NotificationLogMapper
    {
        public static NotificationLogDTO ToDto(this NotificationLog log)
        {
            return new NotificationLogDTO
            {
                Id = log.Id,
                ContactNumber = log.ContactNumber,
                Message = log.Message,
                Type = log.Type,
                Status = log.Status,
                SentAt = log.SentAt
            };
        }

        public static List<NotificationLogDTO> ToDtoList(this IEnumerable<NotificationLog> logs)
        {
            return logs.Select(log => log.ToDto()).ToList();
        }
    }
} 