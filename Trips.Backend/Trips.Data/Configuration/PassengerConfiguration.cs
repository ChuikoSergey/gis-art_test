using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Core.Entities;
using Trips.Data.Configuration.Base;

namespace Trips.Data.Configuration;

public class PassengerConfiguration : BaseEntityTypeConfiguration<Passenger>
{
    public override void Configure(EntityTypeBuilder<Passenger> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).IsRequired();
    }
}
