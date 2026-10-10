using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");
        builder.HasKey(vehicle => vehicle.VehicleId);
        builder.Property(vehicle => vehicle.VehicleId).HasColumnName("id");
        builder.Property(vehicle => vehicle.Brand).HasColumnName("brand").IsRequired().HasMaxLength(100);
        builder.Property(vehicle => vehicle.Model).HasColumnName("model").IsRequired().HasMaxLength(100);
        builder.Property(vehicle => vehicle.ManufactureYear).HasColumnName("manufacture_year").IsRequired();
        builder.Property(vehicle => vehicle.Mileage).HasColumnName("mileage").IsRequired();
        builder.Property(vehicle => vehicle.Price).HasColumnName("price").IsRequired().HasPrecision(12, 2);
        builder.HasOne(vehicle => vehicle.Dealership).WithMany(dealership => dealership.Vehicles).HasForeignKey(vehicle => vehicle.DealershipId).IsRequired();
        builder.Property(vehicle => vehicle.DealershipId).HasColumnName("dealership_id");
    }
}