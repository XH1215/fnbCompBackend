//using Microsoft.AspNetCore.Mvc;

//using orderAPI.Results.Queue;

//using System;
//using System.Threading.Tasks;

//namespace orderAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class QueueController : ControllerBase
//    {
//        private readonly IQueueService _queueService;

//        public QueueController(IQueueService queueService)
//        {
//            _queueService = queueService;
//        }

//        /// <summary>
//        /// 加入队列
//        /// </summary>
//        [HttpPost("join")]
//        public async Task<ActionResult<JoinQueueResult>> JoinQueue([FromBody] JoinQueueRequest request)
//        {
//            try
//            {
//                var queueEntry = await _queueService.JoinQueueAsync(request.ContactNumber, request.OutletId, request.NumberOfGuests, request.SpecialRequests);
//                return Ok(new JoinQueueResult(true, "Queue joined successfully", queueEntry));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new JoinQueueResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 获取当前队列状态（根据联系电话和 outletId）
//        /// </summary>
//        [HttpGet("status")]
//        public async Task<ActionResult<GetQueueStatusResult>> GetQueueStatus([FromQuery] string contactNumber, [FromQuery] int outletId)
//        {
//            try
//            {
//                var queueEntry = await _queueService.GetQueueStatusAsync(contactNumber, outletId);
//                if (queueEntry == null)
//                {
//                    return NotFound(new GetQueueStatusResult(false, "No waiting queue found"));
//                }
//                return Ok(new GetQueueStatusResult(true, "Queue status retrieved successfully", queueEntry));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new GetQueueStatusResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 取消队列
//        /// </summary>
//        [HttpDelete("cancel/{queueId}")]
//        public async Task<ActionResult<CancelQueueResult>> CancelQueue([FromRoute] int queueId)
//        {
//            try
//            {
//                bool result = await _queueService.CancelQueueAsync(queueId);
//                if (!result)
//                {
//                    return NotFound(new CancelQueueResult(false, "Queue entry not found or cancellation failed"));
//                }
//                return Ok(new CancelQueueResult(true, "Queue cancelled successfully", result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new CancelQueueResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 获取所有等待中的队列记录（按 outletId 查询）
//        /// </summary>
//        [HttpGet("waiting/{outletId}")]
//        public async Task<ActionResult<GetAllWaitingQueueEntriesResult>> GetAllWaitingQueueEntries([FromRoute] int outletId)
//        {
//            try
//            {
//                var queueEntries = await _queueService.GetAllWaitingQueueEntriesAsync(outletId);
//                return Ok(new GetAllWaitingQueueEntriesResult(true, "Retrieved all waiting queue entries", queueEntries));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new GetAllWaitingQueueEntriesResult(false, ex.Message));
//            }
//        }

//        /// <summary>
//        /// 通知下一个客户
//        /// </summary>
//        [HttpPost("notify")]
//        public async Task<ActionResult<NotifyNextCustomerResult>> NotifyNextCustomer([FromBody] Request.Queue.NotifyNextCustomerRequest request)
//        {
//            try
//            {
//                bool result = await _queueService.NotifyNextCustomerAsync(request.OutletId);
//                if (!result)
//                {
//                    return NotFound(new NotifyNextCustomerResult(false, "No waiting queue entry available to notify"));
//                }
//                return Ok(new NotifyNextCustomerResult(true, "Next customer notified successfully", result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new NotifyNextCustomerResult(false, ex.Message));
//            }
//        }
//    }
//}
