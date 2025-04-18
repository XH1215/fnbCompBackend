namespace orderAPI.Results.Staff
{
    public class UpdateStaffResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UpdateStaffResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}