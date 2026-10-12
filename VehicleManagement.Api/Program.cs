using Microsoft.EntityFrameworkCore;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Services;
using VehicleManagement.Infrastructure.Persistence;
using VehicleManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

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

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDealershipRepository, DealershipRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
