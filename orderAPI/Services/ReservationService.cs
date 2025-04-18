// Services/ReservationService.cs
using orderAPI.Repositories;
using orderAPI.Requests.Reservation;
using orderAPI.Results.Reservation;

namespace orderAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateReservationResult> CreateReservationAsync(CreateReservationRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.ContactNumber))
                return new CreateReservationResult(false, "ContactNumber is required");

            if (req.OutletId <= 0)
                return new CreateReservationResult(false, "OutletId must be > 0");

            if (req.Guests <= 0)
                return new CreateReservationResult(false, "NumberOfGuests must be at least 1");

            if (req.Date < DateTime.UtcNow.Date)
                return new CreateReservationResult(false, "ReservationDate cannot be in the past");

            try
            {
                var r = await _repo.CreateReservationAsync(
                    req.ContactNumber,
                    req.OutletId,
                    req.Date,
                    req.Time,
                    req.Guests,
                    req.SpecialRequests);

                return new CreateReservationResult(true, "Reservation created");
            }
            catch (Exception ex)
            {
                return new CreateReservationResult(false, $"Failed to create reservation: {ex.Message}");
            }
        }

        public async Task<GetReservationsResult> GetReservationsAsync(GetReservationsByContactRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.ContactNumber))
                return new GetReservationsResult(false, "ContactNumber is required");

            var list = await _repo.GetReservationsByContactAsync(req.ContactNumber);
            return new GetReservationsResult(
                true,
                $"Found {list.Count} reservations",
                list);
        }

        public async Task<UpdateReservationResult> UpdateReservationAsync(UpdateReservationRequest req)
        {
            if (req.Id <= 0)
                return new UpdateReservationResult(false, "ReservationId is invalid");

            if (req.NumberOfGuests <= 0)
                return new UpdateReservationResult(false, "NumberOfGuests must be at least 1");

            try
            {
                var updated = await _repo.UpdateReservationAsync(
                    req.Id,
                    req.ReservationDate,
                    req.ReservationTime,
                    req.NumberOfGuests,
                    req.SpecialRequests);

                return updated == null
                    ? new UpdateReservationResult(false, "Reservation not found")
                    : new UpdateReservationResult(true, "Reservation updated");
            }
            catch (Exception ex)
            {
                return new UpdateReservationResult(false, $"Failed to update reservation: {ex.Message}");
            }
        }

        public async Task<CancelReservationResult> CancelReservationAsync(CancelReservationRequest req)
        {
            if (req.ReservationId <= 0)
                return new CancelReservationResult(false, "ReservationId is invalid");

            var ok = await _repo.CancelReservationAsync(req.ReservationId);
            return ok
                ? new CancelReservationResult(true, "Reservation cancelled")
                : new CancelReservationResult(false, "Reservation not found");
        }
    }
}
