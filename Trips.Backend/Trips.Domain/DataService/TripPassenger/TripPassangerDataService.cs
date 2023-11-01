using Trips.Core.Data.Repository;
using Trips.Core.DataService.Passenger;
using Trips.Core.DataService.TripPassenger;
using Trips.Domain.DataService.Base;

namespace Trips.Domain.DataService.Passenger;

public class TripPassangerDataService : BaseDataService<Core.Entities.TripPassenger>, ITripPassengerDataService
{
    public TripPassangerDataService(IRepository<Core.Entities.TripPassenger> repository) : base(repository)
    {
    }
}
