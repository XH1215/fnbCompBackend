using System.Collections.Generic;
using OrderAPI.Models;

namespace OrderAPI.Results.Queue
{
	public class GetAllWaitingQueueEntriesResult : BaseResult<List<Queue>>
	{
		public GetAllWaitingQueueEntriesResult(bool success, string message, List<Queue> data)
			: base(success, message, data)
		{
		}

		public GetAllWaitingQueueEntriesResult(bool success, string message)
			: base(success, message)
		{
		}
	}
}
