namespace orderAPI.Requests.Outlet
{
    public class CreateOutletRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string OperatingHours { get; set; }
        public int Capacity { get; set; }
    }
}