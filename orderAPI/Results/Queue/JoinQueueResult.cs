using OrderAPI.Models;

namespace OrderAPI.Results.Queue
{
    public class JoinQueueResult : BaseResult<Queue>
    {
        public JoinQueueResult(bool success, string message, Queue data)
            : base(success, message, data)
        {
        }

        public JoinQueueResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
