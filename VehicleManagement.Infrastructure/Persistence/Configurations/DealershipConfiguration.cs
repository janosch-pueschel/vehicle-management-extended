using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Infrastructure.Persistence.Configurations;

public class DealershipConfiguration : IEntityTypeConfiguration<Dealership>
{
    public void Configure(EntityTypeBuilder<Dealership> builder)
    {
        builder.ToTable("dealerships");
        builder.HasKey(dealership => dealership.DealershipId);
        builder.Property(dealership => dealership.DealershipId).HasColumnName("id");
        builder.Property(dealership => dealership.Name).HasColumnName("name").IsRequired().HasMaxLength(150);
        builder.Property(dealership => dealership.City).HasColumnName("city").IsRequired().HasMaxLength(100);
    }
}