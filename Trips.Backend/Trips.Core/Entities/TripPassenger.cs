using Trips.Core.Entities.Base;

namespace Trips.Core.Entities;

public class TripPassenger : IEntity
{
    public Guid Id { get; set; }

    public Guid PassengerId { get; set; }
    public Guid TripId { get; set; }

    public Passenger Passenger { get; set; }
    public Trip Trip { get; set; }
}
