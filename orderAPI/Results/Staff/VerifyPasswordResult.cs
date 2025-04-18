namespace orderAPI.Results.Staff
{
    public class VerifyPasswordResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }

        public VerifyPasswordResult(bool success, string message, bool isValid = false)
        {
            Success = success;
            Message = message;
            IsValid = isValid;
        }
    }
}