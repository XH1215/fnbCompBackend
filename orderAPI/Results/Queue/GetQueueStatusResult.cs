using OrderAPI.Models;

namespace OrderAPI.Results.Queue
{
    public class GetQueueStatusResult : BaseResult<Queue>
    {
        public GetQueueStatusResult(bool success, string message, Queue data)
            : base(success, message, data)
        {
        }

        public GetQueueStatusResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
