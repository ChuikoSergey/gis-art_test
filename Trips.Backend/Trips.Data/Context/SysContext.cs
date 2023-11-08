using Microsoft.EntityFrameworkCore;
using Trips.Data.Entities;

namespace Trips.Data.Context;

public partial class SysContext : DbContext, ISysContext
{
    public SysContext()
    {
    }

    public SysContext(DbContextOptions<SysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Trip>(entity =>
        {
            entity
                .ToTable("trip")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.DriverId)
                .HasColumnType("int")
                .HasColumnName("driver_id");
            entity.Property(e => e.Dropoff)
                .HasMaxLength(19)
                .HasColumnName("dropoff");
            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .IsRequired();
            entity.Property(e => e.Pickup)
                .HasMaxLength(19)
                .HasColumnName("pickup");

            entity.HasKey("Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
