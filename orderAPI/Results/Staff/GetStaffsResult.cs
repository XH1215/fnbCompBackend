using orderAPI.DTO;
using orderAPI.Models;
using System.Collections.Generic;

namespace orderAPI.Results.Staff
{
    public class GetStaffsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<StaffDTO>? Staffs { get; set; }

        public GetStaffsResult(bool success, string message, IEnumerable<StaffDTO>? staffs = null)
        {
            Success = success;
            Message = message;
            Staffs = staffs;
        }
    }
} 