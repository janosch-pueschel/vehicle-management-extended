using Microsoft.EntityFrameworkCore;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Dealership> Dealerships { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}