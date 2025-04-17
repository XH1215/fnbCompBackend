// Controllers/ReservationController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using orderAPI.Requests.Reservation;
using orderAPI.Results.Reservation;
using orderAPI.Services;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// 创建预约
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreateReservationResult>> Create([FromBody] ReservationCreateRequest req)
        {
            var result = await _reservationService.CreateReservationAsync(req);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// 查询用户所有预约
        /// </summary>
        [HttpPost("get")]
        public async Task<ActionResult<GetReservationsResult>> Get([FromBody] ReservationGetRequest req)
        {
            var result = await _reservationService.GetReservationsAsync(req);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// 更新预约
        /// </summary>
        [HttpPost("update")]
        public async Task<ActionResult<UpdateReservationResult>> Update([FromBody] ReservationUpdateRequest req)
        {
            var result = await _reservationService.UpdateReservationAsync(req);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        [HttpPost("cancel")]
        public async Task<ActionResult<CancelReservationResult>> Cancel([FromBody] ReservationCancelRequest req)
        {
            var result = await _reservationService.CancelReservationAsync(req);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
