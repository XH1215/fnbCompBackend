namespace orderAPI.Requests.Notification
{
    public class SendMessageRequest
    {
        public string Phone { get; set; }
        public string Template { get; set; }

        public string CustomerName { get; set; }

        public int Queue { get; set; }
        public string Type { get; set; } = "WhatsApp"; // Default to WhatsApp, but could be SMS, etc.
    }
}