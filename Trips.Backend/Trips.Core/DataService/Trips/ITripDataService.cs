using Trips.Core.DataService.BaseCrudService;
using Trips.Core.DTO.Driver;
using Trips.Core.DTO.SearchableRequest;
using Trips.Core.DTO.SearchableRequest.Trip;
using Trips.Core.DTO.Trip;
using Trips.Data.Entities;

namespace Trips.Core.DataService.Trips;

public interface ITripDataService : IBaseDataService<Trip>
{
    Task<List<int>> GetAllDriversIds();
    Task<List<DriverListTableDto>> GetDriversTableDataAsync(SearchableRequest request);
    Task<List<DriverTripTimeDto>> GetTripTimeByDrivers(IEnumerable<int> driverIds);
    Task<List<TripTableDto>> GetTableDataByDriverAsync(TripTableDtoRequest? request);
}
