namespace orderAPI.Services
{
    public interface IWhatsAppService
    {
        Task<bool> SendTemplateMessageAsync(string toPhoneNumber, string templateName, string languageCode = "en_US");
    }

}
