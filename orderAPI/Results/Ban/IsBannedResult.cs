namespace orderAPI.Results.Ban
{
    public class IsBannedResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsBanned { get; set; }

        public IsBannedResult()
        {
        }

        public IsBannedResult(bool success, string message, bool isBanned)
        {
            Success = success;
            Message = message;
            IsBanned = isBanned;
        }
    }
}