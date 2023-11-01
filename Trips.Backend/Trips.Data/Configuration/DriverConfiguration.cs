using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Core.Entities;
using Trips.Data.Configuration.Base;

namespace Trips.Data.Configuration;

public class DriverConfiguration : BaseEntityTypeConfiguration<Driver>
{
    public override void Configure(EntityTypeBuilder<Driver> builder)
    {
        base.Configure(builder);

        builder.Property(d => d.Name)
            .IsRequired();
    }
}
