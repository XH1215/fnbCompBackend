namespace orderAPI.Results.Outlet
{
    public class UpdateOutletResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UpdateOutletResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}