using Microsoft.AspNetCore.Mvc;
using orderAPI.Models;
using orderAPI.Requests.Staff;
using orderAPI.Results.Staff;
using orderAPI.Services;
using System;
using System.Threading.Tasks;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateStaffResult>> CreateStaff([FromBody] CreateStaffRequest request)
        {
            try
            {
                var staff = await _staffService.CreateStaffAsync(
                    request.Name,
                    request.Email,
                    request.Password,
                    request.Role,
                    request.OutletId);
                return Ok(new CreateStaffResult(true, "Staff created successfully", staff));
            }
            catch (Exception ex)
            {
                return BadRequest(new CreateStaffResult(false, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStaffResult>> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
                return NotFound(new GetStaffResult(false, "Staff not found"));

            return Ok(new GetStaffResult(true, "Staff retrieved successfully", staff));
        }

        [HttpGet]
        public async Task<ActionResult<GetStaffsResult>> GetAllStaff()
        {
            var staffs = await _staffService.GetAllStaffAsync("OutletStaff");
            return Ok(new GetStaffsResult(true, "Staffs retrieved successfully", staffs));
        }

        [HttpGet("byoutlet/{outletId}")]
        public async Task<ActionResult<GetStaffsResult>> GetStaffByOutlet(int outletId)
        {
            var staffs = await _staffService.GetAllStaffAsync(null,outletId);
            return Ok(new GetStaffsResult(true, "Staffs retrieved successfully", staffs));
        }

        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<GetStaffResult>> GetStaffByEmail(string email)
        {
            var staff = await _staffService.GetStaffByEmailAsync(email);
            if (staff == null)
                return NotFound(new GetStaffResult(false, "Staff not found"));

            return Ok(new GetStaffResult(true, "Staff retrieved successfully", staff));
        }

        [HttpPut("update")]
        public async Task<ActionResult<UpdateStaffResult>> UpdateStaff([FromBody] UpdateStaffRequest request)
        {
            var staff = new Staff
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                OutletId = request.OutletId,
                IsActive = request.IsActive
            };

            var result = await _staffService.UpdateStaffAsync(staff);
            if (!result)
                return NotFound(new UpdateStaffResult(false, "Staff not found or update failed"));

            return Ok(new UpdateStaffResult(true, "Staff updated successfully"));
        }


        [HttpPost("deactivate/{id}")]
        public async Task<ActionResult<UpdateStaffResult>> DeactivateStaff(int id)
        {
            var result = await _staffService.DeactivateStaffAsync(id);
            if (!result)
                return NotFound(new UpdateStaffResult(false, "Staff not found or deactivation failed"));

            return Ok(new UpdateStaffResult(true, "Staff deactivated successfully"));
        }


        //login ?
        [HttpPost("verifypassword")]
        public async Task<ActionResult<VerifyPasswordResult>> VerifyPassword([FromBody] VerifyPasswordRequest request)
        {
            var result = await _staffService.VerifyPasswordAsync(request.Email, request.Password);
            
            return Ok(new VerifyPasswordResult(true, "Password verification completed", result));
        }
    }
} 