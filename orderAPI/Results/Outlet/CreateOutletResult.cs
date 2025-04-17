using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;

namespace orderAPI.Results.Outlet
{
    public class CreateOutletResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public OutletDTO? Outlet { get; set; }

        public CreateOutletResult()
        {
        }

        public CreateOutletResult(bool success, string message, OutletDTO? outlet = null)
        {
            Success = success;
            Message = message;
            
           
        }
    }
} 