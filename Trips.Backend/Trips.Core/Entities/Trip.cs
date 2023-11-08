using Trips.Core.Entities.Base;

namespace Trips.Data.Entities;

public partial class Trip : IEntity
{
    public int Id { get; set; }

    public int DriverId { get; set; }

    public string? Pickup { get; set; }

    public string? Dropoff { get; set; }
}
