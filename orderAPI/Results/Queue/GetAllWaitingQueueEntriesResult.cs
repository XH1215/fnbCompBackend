using System.Collections.Generic;
using orderAPI.Results;
using QueueModel = orderAPI.Models.Queue;

namespace orderAPI.Results.Queue
{
    public class GetAllWaitingQueueEntriesResult : BaseResult<List<QueueModel>>
    {
        public GetAllWaitingQueueEntriesResult(bool success, string message, List<QueueModel> data)
            : base(success, message, data)
        {
        }

        public GetAllWaitingQueueEntriesResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
