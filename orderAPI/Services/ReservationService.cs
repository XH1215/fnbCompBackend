// Services/ReservationService.cs
using orderAPI.Models;
using orderAPI.Repositories;
using orderAPI.Requests.Reservation;
using orderAPI.Results.Reservation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace orderAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateReservationResult> CreateReservationAsync(ReservationCreateRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.CustomerPhone))
                return new CreateReservationResult(false, "ContactNumber is required", 0);

            if (req.OutletId <= 0)
                return new CreateReservationResult(false, "OutletId must be > 0", 0);

            if (req.NumberOfGuests <= 0)
                return new CreateReservationResult(false, "NumberOfGuests must be at least 1", 0);

            if (req.ReservationDate < DateTime.UtcNow.Date)
                return new CreateReservationResult(false, "ReservationDate cannot be in the past", 0);

            try
            {
                var r = await _repo.CreateReservationAsync(
                    req.CustomerPhone,
                    req.OutletId,
                    req.ReservationDate,
                    req.ReservationTime,
                    req.NumberOfGuests,
                    req.SpecialRequests);

                return new CreateReservationResult(true, "Reservation created", r.Id);
            }
            catch (Exception ex)
            {
                return new CreateReservationResult(false, $"Failed to create reservation: {ex.Message}", 0);
            }
        }

        public async Task<GetReservationsResult> GetReservationsAsync(ReservationGetRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.CustomerPhone))
                return new GetReservationsResult(false, "ContactNumber is required", null);

            var list = await _repo.GetReservationsByContactAsync(req.CustomerPhone);
            return new GetReservationsResult(
                true,
                $"Found {list.Count} reservations",
                list);
        }

        public async Task<UpdateReservationResult> UpdateReservationAsync(ReservationUpdateRequest req)
        {
            if (req.ReservationId <= 0)
                return new UpdateReservationResult(false, "ReservationId is invalid");

            if (req.NumberOfGuests <= 0)
                return new UpdateReservationResult(false, "NumberOfGuests must be at least 1");

            try
            {
                var updated = await _repo.UpdateReservationAsync(
                    req.ReservationId,
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

        public async Task<CancelReservationResult> CancelReservationAsync(ReservationCancelRequest req)
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
