namespace Trips.Core.DTO.Driver;

using Trips.Core.Attributes.Searchable;

public class DriverListTableDto
{
    public Guid Id { get; set; }
    [Searchable]
    public double? PayableTime { get; set; }
    [Searchable]
    public string Name { get; set; } = string.Empty;
}
