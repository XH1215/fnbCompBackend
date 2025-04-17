using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;

namespace orderAPI.Results.Outlet
{
    public class GetOutletResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public OutletDTO? Outlet { get; set; }

        public GetOutletResult()
        {
        }

        public GetOutletResult(bool success, string message, OutletDTO? outlet = null)
        {
            Success = success;
            Message = message;
            
     
        }
    }
} 