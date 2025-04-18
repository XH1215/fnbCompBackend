using orderAPI.DTO;

namespace orderAPI.Results.Staff
{
    public class GetStaffResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public StaffDTO? Staff { get; set; }

        public GetStaffResult(bool success, string message, StaffDTO? staff = null)
        {
            Success = success;
            Message = message;
            Staff = staff;
        }
    }
}