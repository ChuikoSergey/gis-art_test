using Trips.Core.Entities.Base;

namespace Trips.Core.Entities;

public class Passenger : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<TripPassenger> Trips { get; set; }
}
