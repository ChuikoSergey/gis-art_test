using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Core.Entities;
using Trips.Data.Configuration.Base;

namespace Trips.Data;

public class TripConfiguration : BaseEntityTypeConfiguration<Trip>
{
    public override void Configure(EntityTypeBuilder<Trip> builder)
    {
        base.Configure(builder);

        builder.HasOne(t => t.Driver)
            .WithMany(d => d.Trips)
            .HasForeignKey(t => t.DriverId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
