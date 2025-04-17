using orderAPI.Results;
using QueueModel = orderAPI.Models.Queue;

namespace orderAPI.Results.Queue
{
    public class GetQueueStatusResult : BaseResult<QueueModel>
    {
        public GetQueueStatusResult(bool success, string message, QueueModel data)
            : base(success, message, data)
        {
        }

        public GetQueueStatusResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
