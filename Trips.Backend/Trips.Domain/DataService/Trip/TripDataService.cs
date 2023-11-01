using Microsoft.EntityFrameworkCore;
using Trips.Core;
using Trips.Core.Data.Repository;
using Trips.Core.DataAdapters.Trip;
using Trips.Core.DataService.Trip;
using Trips.Core.DTO.SearchableRequest.Trip;
using Trips.Core.DTO.Trip;
using Trips.Domain.DataService.Base;
using Trips.Core.Extensions.Queryable;

namespace Trips.Domain.DataService.Trip;

public class TripDataService : BaseDataService<Core.Entities.Trip>, ITripDataService
{
    public TripDataService(IRepository<Core.Entities.Trip> repository) : base(repository)
    {
    }

    public Task<List<DriverTripTimeDto>> GetTripTimeByDrivers(IEnumerable<Guid> driverIds)
    {
        if (driverIds == null || !driverIds.Any())
        {
            return Task.FromResult(new List<DriverTripTimeDto>());
        }
        return _repository.Data
            .Where(t => driverIds.Contains(t.DriverId) && t.Passengers.Any())
            .Select(t => new DriverTripTimeDto
            {
                DriverId = t.DriverId,
                TripStartTimestamp = t.TripStartTimestamp,
                TripEndTimestamp = t.TripEndTimestamp
            })
            .ToListAsync();
        
    }

    public Task<List<TripTableDto>> GetTableDataByDriverAsync(TripTableDtoRequest request)
    {
        if (request == null || !request.DriverId.HasValue || request.DriverId == Guid.Empty)
        {
            return Task.FromResult(new List<TripTableDto>());
        }

        return _repository.Data
            .Where(t => t.DriverId == request.DriverId)
            .SearchBy<Core.Entities.Trip, TripTableDto>(request?.SearchBy)
            .OrderBy(request)
            .Select(TripAdapters.EntityToTableDto)
            .ToListAsync();
    }
}
