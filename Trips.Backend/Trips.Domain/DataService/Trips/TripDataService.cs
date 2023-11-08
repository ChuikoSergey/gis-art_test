using Microsoft.EntityFrameworkCore;
using Trips.Core;
using Trips.Core.Data.Repository;
using Trips.Core.DTO.SearchableRequest.Trip;
using Trips.Core.DTO.Trip;
using Trips.Domain.DataService.Base;
using Trips.Core.Extensions.Queryable;
using Trips.Data.Entities;
using Trips.Core.DTO.Driver;
using Trips.Core.DTO.SearchableRequest;
using Trips.Core.DataService.Trips;
using Trips.Core.DataAdapters.Trips;
using System.Linq;

namespace Trips.Domain.DataService.Trips;

public class TripDataService : BaseDataService<Trip>, ITripDataService
{
    public TripDataService(IRepository<Trip> repository) : base(repository)
    {
    }

    public Task<List<DriverTripTimeDto>> GetTripTimeByDrivers(IEnumerable<int> driverIds)
    {
        if (driverIds == null || !driverIds.Any())
        {
            return Task.FromResult(new List<DriverTripTimeDto>());
        }
        return _repository.Data
            .Where(t => driverIds.Contains(t.DriverId))
            .Select(t => new DriverTripTimeDto
            {
                DriverId = t.DriverId,
                Pickup = t.Pickup,
                Dropoff = t.Dropoff
            })
            .ToListAsync();
        
    }

    public async Task<List<DriverListTableDto>> GetDriversTableDataAsync(SearchableRequest request)
    {
        var resultData = await  _repository.Data
            .SearchBy<Trip, DriverListTableDto>(request?.SearchBy)
            .OrderBy(request)
            .Select(t => t.DriverId)
            .Distinct()
            .ToListAsync();
        return resultData.Select(id => new DriverListTableDto 
        {
            Id = id
        }).ToList();
    }

    public Task<List<TripTableDto>> GetTableDataByDriverAsync(TripTableDtoRequest request)
    {
        if (request == null || !request.DriverId.HasValue)
        {
            return Task.FromResult(new List<TripTableDto>());
        }

        return _repository.Data
            .Where(t => t.DriverId == request.DriverId)
            .SearchBy<Trip, TripTableDto>(request?.SearchBy)
            .OrderBy(request)
            .Select(TripAdapters.EntityToTripTableDto)
            .ToListAsync();
    }

    public Task<List<int>> GetAllDriversIds()
    {
        return _repository.Data.Select(t => t.DriverId).ToListAsync();
    }
}
