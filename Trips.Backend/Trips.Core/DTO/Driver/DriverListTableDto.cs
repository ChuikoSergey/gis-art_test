namespace Trips.Core.DTO.Driver;

using Trips.Core.Attributes.Searchable;

public class DriverListTableDto
{
    [Searchable("DriverId")]
    public int Id { get; set; }
    public double? PayableTime { get; set; }
}
