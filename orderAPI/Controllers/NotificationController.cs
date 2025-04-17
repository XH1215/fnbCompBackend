using Microsoft.AspNetCore.Mvc;
using orderAPI.Requests.Notification;
using orderAPI.Results.Notification;
using orderAPI.Services;
using System;
using System.Threading.Tasks;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<SendMessageResult>> SendMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                var result = await _notificationService.SendWhatsAppMessageAsync(
                    request.Phone,
                    request.Message,
                    request.Type);

                return Ok(new SendMessageResult
                {
                    Success = true,
                    Message = result ? "Message sent successfully" : "Failed to send message",
                    IsDelivered = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new SendMessageResult
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    IsDelivered = false
                });
            }
        }

        [HttpGet("logs")]
        public async Task<ActionResult<GetNotificationLogsResult>> GetLogs([FromQuery] string? phone = null, [FromQuery] string? type = null)
        {
            try
            {
                var logs = await _notificationService.GetLogsAsync(phone, type);
                return Ok(new GetNotificationLogsResult
                {
                    Success = true,
                    Message = "Notification logs retrieved successfully",
                    Logs = logs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GetNotificationLogsResult
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost("resend/{logId}")]
        public async Task<ActionResult<ResendNotificationResult>> ResendNotification(int logId)
        {
            try
            {
                var result = await _notificationService.ResendFailedNotificationAsync(logId);
                
                if (!result)
                {
                    return BadRequest(new ResendNotificationResult
                    {
                        Success = false,
                        Message = "Failed to resend notification. The log may not exist or the message was already successfully sent.",
                        IsResent = false
                    });
                }

                return Ok(new ResendNotificationResult
                {
                    Success = true,
                    Message = "Notification resent successfully",
                    IsResent = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResendNotificationResult
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    IsResent = false
                });
            }
        }
    }
} 