using Trips.Core.Attributes.Searchable;

namespace Trips.Core.DTO.Trip;

public class TripTableDto
{
    [Searchable]
    public int Id { get; set; }
    [Searchable]
    public int DriverId { get; set; }    
    [Searchable]
    public string? Pickup { get; set; }
    [Searchable]
    public string? Dropoff { get; set; }
}
