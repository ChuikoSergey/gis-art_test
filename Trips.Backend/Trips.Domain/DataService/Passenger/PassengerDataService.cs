using Trips.Core.Data.Repository;
using Trips.Core.DataService.Passenger;
using Trips.Domain.DataService.Base;

namespace Trips.Domain.DataService.Passenger;

public class PassengerDataService : BaseDataService<Core.Entities.Passenger>, IPassengerDataService
{
    public PassengerDataService(IRepository<Core.Entities.Passenger> repository) : base(repository)
    {
    }
}
