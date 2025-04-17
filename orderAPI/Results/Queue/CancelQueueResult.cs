using orderAPI.Results;

namespace orderAPI.Results.Queue
{
    public class CancelQueueResult : BaseResult<bool>
    {
        public CancelQueueResult(bool success, string message, bool data)
            : base(success, message, data)
        {
        }

        public CancelQueueResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
