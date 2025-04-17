using orderAPI.Results;
using QueueModel = orderAPI.Models.Queue;

namespace orderAPI.Results.Queue
{
    public class JoinQueueResult : BaseResult<QueueModel>
    {
        public JoinQueueResult(bool success, string message, QueueModel data)
            : base(success, message, data)
        {
        }

        public JoinQueueResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
