using System.Linq.Expressions;
using Trips.Core.DTO.Driver;

namespace Trips.Core.DataAdapters.Driver;

public static class DriverAdapters
{
    public static Expression<Func<Entities.Driver, DriverListTableDto>> EntityToListTableDto = d => new DriverListTableDto
        {
            Id = d.Id,
            Name = d.Name,
            PayableTime = d.PayableTime
        };
}
