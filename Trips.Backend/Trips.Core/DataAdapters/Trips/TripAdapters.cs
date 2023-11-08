using System.Linq.Expressions;
using Trips.Core.DTO.Driver;
using Trips.Core.DTO.Trip;
using Trips.Data.Entities;

namespace Trips.Core.DataAdapters.Trips;

public static class TripAdapters
{
    public static Expression<Func<Trip, TripTableDto>> EntityToTripTableDto = t => new TripTableDto
    {
        Id = t.Id,
        DriverId = t.DriverId,
        Pickup = t.Pickup,
        Dropoff = t.Dropoff
    };
}
