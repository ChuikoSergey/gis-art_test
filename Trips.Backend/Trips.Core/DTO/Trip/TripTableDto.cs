using Trips.Core.Attributes.Searchable;

namespace Trips.Core.DTO.Trip;

public class TripTableDto
{
    [Searchable]
    public Guid Id { get; set; }
    public DateTime TripStartTimestamp { get; set; }
    public DateTime TripEndTimestamp { get; set; }
    [Searchable]
    public string StartPlaceName { get; set; } = string.Empty;
    [Searchable]
    public string DestinationPlaceName { get; set; } = string.Empty;
}
