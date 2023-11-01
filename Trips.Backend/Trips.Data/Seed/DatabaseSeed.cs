using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Trips.Data.Seed;

public static class DatabaseSeed
{
    private static void SeedEntityTable<TEntity>(ModelBuilder modelBuilder, string filePath) where TEntity : class
    {
        if (File.Exists(filePath))
        {
            var seedData = default(List<TEntity>);
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<TEntity>>(json);
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
                seedData = result;
            }
            modelBuilder.Entity<TEntity>().HasData(seedData);
        }
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        
        SeedEntityTable<Core.Entities.Driver>(modelBuilder, @"./Seed/Data/driver_seed.json");
        SeedEntityTable<Core.Entities.Trip>(modelBuilder, @"./Seed/Data/trip_seed.json");
        SeedEntityTable<Core.Entities.Passenger>(modelBuilder, @"./Seed/Data/passenger_seed.json");
        SeedEntityTable<Core.Entities.TripPassenger>(modelBuilder, @"./Seed/Data/tripPassenger_seed.json");
    }
}
