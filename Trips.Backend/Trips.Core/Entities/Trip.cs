using System.Security;
using Trips.Core.Entities.Base;

namespace Trips.Core.Entities;

public class Trip : IEntity
{
    public Guid Id { get; set; }
    public DateTime TripStartTimestamp { get; set; }
    public DateTime TripEndTimestamp { get; set; }
    public string StartPlaceName { get; set; } = string.Empty;
    public string DestinationPlaceName { get; set; } = string.Empty;
    public Guid DriverId { get; set; }

    public Driver Driver { get; set; }
    public ICollection<TripPassenger> Passengers { get; set; }

    public override string ToString()
    {
        return $"|Id:{Id}, TripStartTimestamp: {TripStartTimestamp}, TripEndTimestamp: {TripStartTimestamp}, StartPlace: {StartPlaceName}, DestinationPlaceName: {DestinationPlaceName}, DriverId: {DriverId}|";
    }
}
