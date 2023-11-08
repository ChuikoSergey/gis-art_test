namespace Trips.Core.DTO.Trip;

public class DriverTripTimeDto
{
    public int DriverId { get; set; }
    public string? Pickup { get; set; }
    public string? Dropoff { get; set; }
}
