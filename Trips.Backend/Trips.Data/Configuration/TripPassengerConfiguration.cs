using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Core.Entities;
using Trips.Data.Configuration.Base;

namespace Trips.Data.Configuration;

public class TripPassengerConfiguration : BaseEntityTypeConfiguration<TripPassenger>
{
    public void Configure(EntityTypeBuilder<TripPassenger> builder)
    {
        base.Configure(builder);

        builder.HasOne(tp => tp.Passenger)
            .WithMany(p => p.Trips)
            .HasForeignKey(tp => tp.PassengerId);

        builder.HasOne(tp => tp.Trip)
            .WithMany(p => p.Passengers)
            .HasForeignKey(tp => tp.TripId);
    }
}
