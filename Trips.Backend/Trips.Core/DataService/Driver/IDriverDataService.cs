using Trips.Core.DataService.BaseCrudService;
using Trips.Core.DTO.Driver;
using Trips.Core.DTO.SearchableRequest;

namespace Trips.Core.DataService.Driver;

public interface IDriverDataService : IBaseDataService<Entities.Driver>
{
    Task<List<Entities.Driver>> GetAllAsync();
    Task<List<DriverListTableDto>> GetDriversTableDataAsync(SearchableRequest searchableRequest);
}
