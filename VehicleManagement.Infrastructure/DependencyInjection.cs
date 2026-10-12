using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Infrastructure.Persistence;
using VehicleManagement.Infrastructure.Repositories;

namespace VehicleManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            );

            if (isDevelopment)
            {
                options.UseSeeding((context, _) =>
                {
                    DbSeeder.Seed((AppDbContext)context);
                });

                options.UseAsyncSeeding((context, _, cancellationToken) =>
                {
                    return DbSeeder.SeedAsync((AppDbContext)context);
                });
            }
        });

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IDealershipRepository, DealershipRepository>();

        return services;
    }
}