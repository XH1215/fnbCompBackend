using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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

    public async Task<bool> SendTemplateMessageAsync(string toPhoneNumber, string customerName, int queue, string templateName, string languageCode = "en_US")
    {
        var token = "EAARXmliTbYwBO0CX6fIhr7B0ZBvLSCPfD2I7sQBrVXZC7y03yIFUrEM0TxvJXy8XxDUwcnpvTQ8Vxh9MbKEZAzaPvrK7pk3RfWhnP7fNPCsqMBTfhigsvJ6X8XygvUQ8ggUpmyU2vsIMDrr4ZA6cxLm8ISw49qfT2BUBrXeP5aPDZAqfjywZCt8aAcoRByUkKkMZAfBN691qEdMvkJimYXZAHlXZAaK7U";
        var phoneNumberId = _configuration["WhatsApp:PhoneNumberId"];
        var url = $"https://graph.facebook.com/v22.0/{phoneNumberId}/messages";

        var payload = new
        {
            messaging_product = "whatsapp",
            to = toPhoneNumber,
            type = "template",
            template = new
            {
                name = "queue_notification",
                language = new { code = languageCode },
                components = new[]
                {
            new
            {
                type = "body",
                parameters = new object[]
                {
                    new { type = "text", text = customerName},
                    new { type = "text", text = queue}
                }
            }
        }
            }
        };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"
        );

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync(url, requestContent);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("WhatsApp API Response: " + responseBody);
        return response.IsSuccessStatusCode;
    }
}
