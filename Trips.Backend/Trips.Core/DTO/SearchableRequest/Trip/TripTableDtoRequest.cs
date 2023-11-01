namespace Trips.Core.DTO.SearchableRequest.Trip;

public class TripTableDtoRequest : SearchableRequest
{
    public Guid? DriverId { get; set; }
}
