namespace OrderAPI.Results.Queue
{
    public class NotifyNextCustomerResult : BaseResult<bool>
    {
        public NotifyNextCustomerResult(bool success, string message, bool data)
            : base(success, message, data)
        {
        }

        public NotifyNextCustomerResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
