using Microsoft.EntityFrameworkCore;
using Trips.Core.Entities;
using Trips.Data.Seed;

namespace Trips.Data.Context;

public class DataContext : DbContext, IDataContext
{
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<TripPassenger> TripPassengers { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
}
