using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using orderAPI.Services;

public class WhatsAppService : IWhatsAppService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WhatsAppService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<bool> SendTemplateMessageAsync(string toPhoneNumber, string templateName, string languageCode = "en_US")
    {
        var token = _configuration["WhatsApp:AccessToken"];
        var phoneNumberId = _configuration["WhatsApp:PhoneNumberId"];
        var url = $"https://graph.facebook.com/v17.0/{phoneNumberId}/messages";

        var payload = new
        {
            messaging_product = "whatsapp",
            to = toPhoneNumber,
            type = "template",
            template = new
            {
                name = templateName,
                language = new { code = languageCode }
            }
        };

        var requestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync(url, requestContent);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("WhatsApp API Response: " + responseBody);
        return response.IsSuccessStatusCode;
    }
}
