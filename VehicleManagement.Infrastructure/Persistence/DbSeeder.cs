using System.Net;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Infrastructure.Persistence;

public static class DbSeeder
{
    private static List<Dealership> CreateDealerships()
    {
        return
        [
            new Dealership
            {
                Name = "Autohaus Leipzig",
                City = "Leipzig"
            },
            new Dealership
            {
                Name = "Autohaus Berlin",
                City = "Berlin"
            },
            new Dealership
            {
                Name = "Autohaus Dresden",
                City = "Dresden"
            }
        ];
    }

    private static List<Vehicle> CreateVehicles(List<Dealership> dealerships)
    {
        var leipzig = dealerships[0];
        var berlin = dealerships[1];
        var dresden = dealerships[2];

        return
        [
            new Vehicle
            {
                Brand = "Volkswagen",
                Model = "Golf",
                ManufactureYear = 2021,
                Mileage = 45000,
                Price = 18900,
                Dealership = leipzig
            },
            new Vehicle
            {
                Brand = "BMW",
                Model = "320i",
                ManufactureYear = 2020,
                Mileage = 62000,
                Price = 24900,
                Dealership = leipzig
            },
            new Vehicle
            {
                Brand = "Skoda",
                Model = "Octavia",
                ManufactureYear = 2022,
                Mileage = 28000,
                Price = 21900,
                Dealership = leipzig
            },
            new Vehicle
            {
                Brand = "Audi",
                Model = "A4",
                ManufactureYear = 2022,
                Mileage = 31000,
                Price = 28900,
                Dealership = berlin
            },
            new Vehicle
            {
                Brand = "Tesla",
                Model = "Model 3",
                ManufactureYear = 2023,
                Mileage = 19000,
                Price = 32900,
                Dealership = berlin
            },
            new Vehicle
            {
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 2021,
                Mileage = 42000,
                Price = 19900,
                Dealership = berlin
            },
            new Vehicle
            {
                Brand = "Mercedes-Benz",
                Model = "C 200",
                ManufactureYear = 2020,
                Mileage = 59000,
                Price = 29900,
                Dealership = dresden
            },
            new Vehicle
            {
                Brand = "Ford",
                Model = "Focus",
                ManufactureYear = 2019,
                Mileage = 78000,
                Price = 13900,
                Dealership = dresden
            },
            new Vehicle
            {
                Brand = "Hyundai",
                Model = "Tucson",
                ManufactureYear = 2022,
                Mileage = 35000,
                Price = 26900,
                Dealership = dresden
            }
        ];
    }

    public static void Seed(AppDbContext context)
    {
        if (context.Dealerships.Any())
        {
            return;
        }

        var dealerships = CreateDealerships();
        var vehicles = CreateVehicles(dealerships);

        context.Dealerships.AddRange(dealerships);
        context.Vehicles.AddRange(vehicles);

        context.SaveChanges();
    }


    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Dealerships.AnyAsync())
        {
            return;
        }

        var dealerships = CreateDealerships();
        var vehicles = CreateVehicles(dealerships);

        context.Dealerships.AddRange(dealerships);
        context.Vehicles.AddRange(vehicles);

        await context.SaveChangesAsync();
    }
}