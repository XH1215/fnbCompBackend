namespace orderAPI.Services
{
    public interface IWhatsAppService
    {
        public Task<bool> SendTemplateMessageAsync(string toPhoneNumber, string customerName, int queue, string templateName = "queue_notification", string languageCode = "en_US");
    }

}
