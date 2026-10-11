using Microsoft.EntityFrameworkCore;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Services;
using VehicleManagement.Infrastructure.Persistence;
using VehicleManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register PostgreSQL and EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );

    if (builder.Environment.IsDevelopment())
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

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<VehicleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
