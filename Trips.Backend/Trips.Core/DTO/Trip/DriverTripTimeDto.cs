namespace Trips.Core;

public class DriverTripTimeDto
{
    public Guid DriverId { get; set; }
    public DateTime TripStartTimestamp { get; set; }
    public DateTime TripEndTimestamp { get; set; }
}
