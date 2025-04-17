using orderAPI.DTO;
using orderAPI.Models;
using System.Collections.Generic;

namespace orderAPI.Helper
{
    public static class OutletMapper
    {
        public static OutletDTO ToDto(this Outlet outlet)
        {
            return new OutletDTO
            {
                Id = outlet.Id,
                Name = outlet.Name,
                Location = outlet.Location,
                OperatingHours = outlet.OperatingHours,
                Capacity = outlet.Capacity,
                CreatedAt = outlet.CreatedAt
            };
        }

        public static List<OutletDTO> ToDtoList(this IEnumerable<Outlet> outlets)
        {
            return outlets.Select(outlet => outlet.ToDto()).ToList();
        }
    }
} 