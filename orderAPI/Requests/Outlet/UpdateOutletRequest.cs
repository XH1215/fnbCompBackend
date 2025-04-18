namespace orderAPI.Requests.Outlet
{
    public class UpdateOutletRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string OperatingHours { get; set; }
        public int Capacity { get; set; }
    }
}