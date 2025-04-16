namespace OrderAPI.Results
{
	public class BaseResult<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public BaseResult(bool success, string message, T data)
		{
			Success = success;
			Message = message;
			Data = data;
		}

		public BaseResult(bool success, string message)
			: this(success, message, default)
		{
		}
	}
}
