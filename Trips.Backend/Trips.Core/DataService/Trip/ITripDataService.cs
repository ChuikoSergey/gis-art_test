using Trips.Core.DataService.BaseCrudService;
using Trips.Core.DTO.SearchableRequest.Trip;
using Trips.Core.DTO.Trip;

namespace Trips.Core.DataService.Trip;

public interface ITripDataService : IBaseDataService<Entities.Trip>
{
    Task<List<DriverTripTimeDto>> GetTripTimeByDrivers(IEnumerable<Guid> driverIds);
    Task<List<TripTableDto>> GetTableDataByDriverAsync(TripTableDtoRequest? request);
}
