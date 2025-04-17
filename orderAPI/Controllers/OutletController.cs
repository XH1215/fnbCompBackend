using Microsoft.AspNetCore.Mvc;
using orderAPI.DTO;
using orderAPI.Models;
using orderAPI.Requests.Outlet;
using orderAPI.Results.Outlet;
using orderAPI.Services;
using System;
using System.Threading.Tasks;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutletController : ControllerBase
    {
        private readonly IOutletService _outletService;

        public OutletController(IOutletService outletService)
        {
            _outletService = outletService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateOutletResult>> CreateOutlet([FromBody] CreateOutletRequest request)
        {
            try
            {
                var outletDto = await _outletService.CreateOutletAsync(
                    request.Name,
                    request.Location,
                    request.OperatingHours,
                    request.Capacity);
                
                return Ok(new CreateOutletResult { Success = true, Message = "Outlet created successfully", Outlet = outletDto });
            }
            catch (Exception ex)
            {
                return BadRequest(new CreateOutletResult { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOutletResult>> GetOutletById(int id)
        {
            var outletDto = await _outletService.GetOutletByIdAsync(id);
            if (outletDto == null)
                return NotFound(new GetOutletResult {Success = false, Message = "Outlet not found" });

            return Ok(new GetOutletResult { Success = true, Message = "Outlet retrieved successfully", Outlet = outletDto });
        }

        [HttpGet]
        public async Task<ActionResult<GetOutletsResult>> GetAllOutlets()
        {
            var outletsDto = await _outletService.GetAllOutletAsync();
            return Ok(new GetOutletsResult { Success = true, Message = "Outlets retrieved successfully", Outlets = outletsDto });
        }

        [HttpPut("update")]
        public async Task<ActionResult<UpdateOutletResult>> UpdateOutlet([FromBody] UpdateOutletRequest request)
        {
            var outlet = new Outlet
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location,
                OperatingHours = request.OperatingHours,
                Capacity = request.Capacity
            };

            var result = await _outletService.UpdateOutletAsync(outlet);
            if (!result)
                return NotFound(new UpdateOutletResult(false, "Outlet not found or update failed"));

            return Ok(new UpdateOutletResult(true, "Outlet updated successfully"));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<DeleteOutletResult>> DeleteOutlet(int id)
        {
            var result = await _outletService.DeleteOutletAsync(id);
            if (!result)
                return NotFound(new DeleteOutletResult(false, "Outlet not found or deletion failed"));

            return Ok(new DeleteOutletResult(true, "Outlet deleted successfully"));
        }
    }
} 