using Trips.Core.Entities.Base;

namespace Trips.Core.Entities;

public class Driver : IEntity
{
    public Guid Id { get; set; }
    public double? PayableTime { get; set; }
    public string Name { get; set; }

    public ICollection<Trip> Trips { get; set; }

    public override string ToString()
    {
        return $"|Id:{Id}, PayableTime: {PayableTime}, Name: {Name}|";
    }
}
