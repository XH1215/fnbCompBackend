using System.Collections.Generic;
using OrderAPI.Models;

namespace OrderAPI.Results.Reservation
{
    public class GetReservationsResult : BaseResult<List<Reservation>>
    {
        public GetReservationsResult(bool success, string message, List<Reservation> data)
            : base(success, message, data)
        {
        }

        public GetReservationsResult(bool success, string message)
            : base(success, message)
        {
        }
    }
}
