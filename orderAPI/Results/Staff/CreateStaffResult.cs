using orderAPI.DTO;

namespace orderAPI.Results.Staff
{
    public class CreateStaffResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public StaffDTO? Staff { get; set; }

        public CreateStaffResult(bool success, string message, StaffDTO? staff = null)
        {
            Success = success;
            Message = message;
            Staff = staff;
        }
    }
} 