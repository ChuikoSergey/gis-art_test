using System.Linq.Expressions;
using Trips.Core.DTO.Trip;

namespace Trips.Core.DataAdapters.Trip;

public static class TripAdapters
{
    public static Expression<Func<Entities.Trip, TripTableDto>> EntityToTableDto = t => new TripTableDto
    {
        Id = t.Id,
        DestinationPlaceName = t.DestinationPlaceName,
        StartPlaceName = t.StartPlaceName,
        TripEndTimestamp = t.TripEndTimestamp,
        TripStartTimestamp = t.TripStartTimestamp
    };
}
