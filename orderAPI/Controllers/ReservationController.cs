//using Microsoft.AspNetCore.Mvc;
//using OrderAPI.Request.Reservation;
//using OrderAPI.Results.Reservation;
//using OrderAPI.Services;
//using System;
//using System.Threading.Tasks;

//namespace OrderAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ReservationController : ControllerBase
//    {
//        private readonly IReservationService _reservationService;

//        public ReservationController(IReservationService reservationService)
//        {
//            _reservationService = reservationService;
//        }

//        /// <summary>
//        /// 创建预订
//        /// </summary>
//        [HttpPost("create")]
//        public async Task<ActionResult<CreateReservationResult>> CreateReservation([FromBody] CreateReservationRequest request)
//        {
//            try
//            {
//                var reservation = await _reservationService.CreateReservationAsync(
//                    request.ContactNumber,
//                    request.OutletId,
//                    request.Date,
//                    request.Time,
//                    request.Guests,
//                    request.SpecialRequests);
//                return Ok(new CreateReservationResult(true, "Reservation created successfully", reservation));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new CreateReservationResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 根据联系方式获取预订记录
//        /// </summary>
//        [HttpGet("bycontact")]
//        public async Task<ActionResult<GetReservationsResult>> GetReservationsByContact([FromQuery] string contactNumber)
//        {
//            try
//            {
//                var reservations = await _reservationService.GetReservationsByContactAsync(contactNumber);
//                return Ok(new GetReservationsResult(true, "Reservations retrieved successfully", reservations));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new GetReservationsResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 更新预订记录
//        /// </summary>
//        [HttpPut("update")]
//        public async Task<ActionResult<UpdateReservationResult>> UpdateReservation([FromBody] UpdateReservationRequest request)
//        {
//            try
//            {
//                // 此处构建实体对象，一般建议传输 DTO 后再映射到实体
//                var updatedReservation = new Models.Reservation
//                {
//                    id = request.Id,
//                    reservation_date = request.ReservationDate,
//                    reservation_time = request.ReservationTime,
//                    number_of_guests = request.NumberOfGuests,
//                    special_requests = request.SpecialRequests
//                };

//                bool result = await _reservationService.UpdateReservationAsync(updatedReservation);
//                if (!result)
//                {
//                    return NotFound(new UpdateReservationResult(false, "Reservation not found or update failed"));
//                }
//                return Ok(new UpdateReservationResult(true, "Reservation updated successfully", result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new UpdateReservationResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 取消预订
//        /// </summary>
//        [HttpDelete("cancel/{reservationId}")]
//        public async Task<ActionResult<CancelReservationResult>> CancelReservation([FromRoute] int reservationId)
//        {
//            try
//            {
//                bool result = await _reservationService.CancelReservationAsync(reservationId);
//                if (!result)
//                {
//                    return NotFound(new CancelReservationResult(false, "Reservation not found or cancellation failed"));
//                }
//                return Ok(new CancelReservationResult(true, "Reservation cancelled successfully", result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new CancelReservationResult(false, ex.Message));
//            }
//        }
//    }
//}
