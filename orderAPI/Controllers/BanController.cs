using Microsoft.AspNetCore.Mvc;
using orderAPI.Requests.Ban;
using orderAPI.Results.Ban;
using orderAPI.Services;
using System;
using System.Threading.Tasks;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanController : ControllerBase
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }

        [HttpPost("ban")]
        public async Task<ActionResult<BanCustomerResult>> BanCustomer([FromBody] BanCustomerRequest request)
        {
            try
            {
                var bannedCustomerDto = await _banService.BanCustomerAsync(request.Phone, request.Reason);
                return Ok(new BanCustomerResult 
                { 
                    Success = true, 
                    Message = "Customer banned successfully", 
                    BannedCustomer = bannedCustomerDto 
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new BanCustomerResult 
                { 
                    Success = false, 
                    Message = ex.Message 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BanCustomerResult 
                { 
                    Success = false, 
                    Message = $"An error occurred: {ex.Message}" 
                });
            }
        }

        [HttpDelete("unban/{id}")]
        public async Task<ActionResult<UnbanCustomerResult>> UnbanCustomer(int id)
        {
            var result = await _banService.UnbanCustomerAsync(id);
            if (!result)
                return NotFound(new UnbanCustomerResult 
                { 
                    Success = false, 
                    Message = "Banned customer not found or unban failed" 
                });

            return Ok(new UnbanCustomerResult 
            { 
                Success = true, 
                Message = "Customer unbanned successfully" 
            });
        }

        [HttpGet("check/{phone}")]
        public async Task<ActionResult<IsBannedResult>> CheckIfBanned(string phone)
        {
            var isBanned = await _banService.IsBannedAsync(phone);
            return Ok(new IsBannedResult 
            { 
                Success = true, 
                Message = isBanned ? "Phone number is banned" : "Phone number is not banned", 
                IsBanned = isBanned 
            });
        }

        [HttpGet]
        public async Task<ActionResult<GetBannedCustomersResult>> GetAllBannedCustomers()
        {
            var bannedCustomers = await _banService.GetAllBannedCustomersAsync();
            return Ok(new GetBannedCustomersResult 
            { 
                Success = true, 
                Message = "Banned customers retrieved successfully", 
                BannedCustomers = bannedCustomers 
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BanCustomerResult>> GetBannedCustomerById(int id)
        {
            var bannedCustomer = await _banService.GetBannedCustomerByIdAsync(id);
            if (bannedCustomer == null)
                return NotFound(new BanCustomerResult 
                { 
                    Success = false, 
                    Message = "Banned customer not found" 
                });

            return Ok(new BanCustomerResult 
            { 
                Success = true, 
                Message = "Banned customer retrieved successfully", 
                BannedCustomer = bannedCustomer 
            });
        }
    }
} 