namespace orderAPI.Results.Ban
{
    public class UnbanCustomerResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UnbanCustomerResult()
        {
        }

        public UnbanCustomerResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}