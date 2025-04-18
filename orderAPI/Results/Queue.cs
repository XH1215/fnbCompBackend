using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public class UpdateQueueResult : BaseResult<QueueModel>
    {
        public UpdateQueueResult(bool success, string message, QueueModel? data = null)
            : base(success, message, data)
        {
        }
    }

    public class CancelQueueResult : BaseResult<bool>
    {
        public CancelQueueResult(bool success, string message)
            : base(success, message)
        {
        }
    }

    public class GetAllWaitingQueueEntriesResult : BaseResult<List<QueueModel>>
    {
        public GetAllWaitingQueueEntriesResult(bool success, string message, List<QueueModel>? data = null)
            : base(success, message, data)
        {
        }
    }

    public class NotifyNextCustomerResult : BaseResult<bool>
    {
        public NotifyNextCustomerResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}