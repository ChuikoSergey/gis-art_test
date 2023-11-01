using Microsoft.EntityFrameworkCore;
using Trips.Core.Entities;

namespace Trips.Data.Context;

public interface IDataContext
{
    DbSet<Driver> Drivers { get; set; }
    DbSet<Trip> Trips { get; set; }
    DbSet<Passenger> Passengers { get; set; }
    DbSet<TripPassenger> TripPassengers { get; set; }
}
