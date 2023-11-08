using Microsoft.EntityFrameworkCore;
using Trips.Data.Entities;

namespace Trips.Data.Context;

public interface ISysContext
{
    DbSet<Trip> Trips { get; set; }
}
