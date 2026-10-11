using Microsoft.EntityFrameworkCore;
using VehicleManagement.Infrastructure.Persistence;

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
