namespace orderAPI.Results.Outlet
{
    public class DeleteOutletResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public DeleteOutletResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}