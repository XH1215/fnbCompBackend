using orderAPI.DTO;
using orderAPI.Helper;
using orderAPI.Models;
using System.Collections.Generic;

namespace orderAPI.Results.Outlet
{
    public class GetOutletsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<OutletDTO>? Outlets { get; set; }

        public GetOutletsResult()
        {
        }

        public GetOutletsResult(bool success, string message, IEnumerable<OutletDTO>? outlets = null)
        {
            Success = success;
            Message = message;
            
        }
    }
} 