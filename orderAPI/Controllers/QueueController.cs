// Controllers/QueueController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using orderAPI.Requests.Queue;
using orderAPI.Results.Queue;
using orderAPI.Services;

namespace orderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        /// <summary>
        /// 加入排队
        /// </summary>
        [HttpPost("join")]
        public async Task<ActionResult<JoinQueueResult>> Join([FromBody] QueueCreateRequest req)
        {
            var result = await _queueService.JoinQueueAsync(req);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// 获取当前排队状态
        /// </summary>
        [HttpPost("status")]
        public async Task<ActionResult<GetQueueStatusResult>> Status([FromBody] QueueStatusRequest req)
        {
            var result = await _queueService.GetQueueStatusAsync(req);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// 取消排队
        /// </summary>
        [HttpPost("cancel")]
        public async Task<ActionResult<CancelQueueResult>> Cancel([FromBody] QueueCancelRequest req)
        {
            var result = await _queueService.CancelQueueAsync(req);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// 获取所有等待列表
        /// </summary>
        [HttpPost("waiting")]
        public async Task<ActionResult<GetAllWaitingQueueEntriesResult>> Waiting([FromBody] AllWaitingQueueRequest req)
        {
            var result = await _queueService.GetAllWaitingQueueEntriesAsync(req);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// 通知下一个客户
        /// </summary>
        [HttpPost("notify-next")]
        public async Task<ActionResult<NotifyNextCustomerResult>> NotifyNext([FromBody] NotifyNextCustomerRequest req)
        {
            var result = await _queueService.NotifyNextCustomerAsync(req);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
