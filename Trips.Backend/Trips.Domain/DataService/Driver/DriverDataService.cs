using Microsoft.EntityFrameworkCore;
using Trips.Core.Data.Repository;
using Trips.Core.DataAdapters.Driver;
using Trips.Core.DataService.Driver;
using Trips.Core.DTO.Driver;
using Trips.Domain.DataService.Base;
using System.Linq.Dynamic.Core;
using Trips.Core.Extensions.Queryable;
using Trips.Core.DTO.SearchableRequest;

namespace Trips.Domain.DataService.Driver;

public class DriverDataService : BaseDataService<Core.Entities.Driver>, IDriverDataService
{
    public DriverDataService(IRepository<Core.Entities.Driver> repository) : base(repository)
    {
    }

    public Task<List<Core.Entities.Driver>> GetAllAsync()
    {
        return _repository.Data.OrderBy("").ToListAsync();
    }

    public Task<List<DriverListTableDto>> GetDriversTableDataAsync(SearchableRequest searchableRequest)
    {
        return _repository.Data
            .SearchBy<Core.Entities.Driver, DriverListTableDto>(searchableRequest?.SearchBy)
            .OrderBy(searchableRequest)
            .Select(DriverAdapters.EntityToListTableDto)
            .ToListAsync();
    }
}
