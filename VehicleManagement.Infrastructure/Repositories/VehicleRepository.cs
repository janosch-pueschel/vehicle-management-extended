using Microsoft.EntityFrameworkCore;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Domain.Entities;
using VehicleManagement.Infrastructure.Persistence;

namespace VehicleManagement.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles
        .AsNoTracking()
        .FirstOrDefaultAsync(vehicle => vehicle.VehicleId == id);
    }

    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return vehicle;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deletedCount = await _context.Vehicles.Where(vehicle => vehicle.VehicleId == id).ExecuteDeleteAsync();

        return deletedCount > 0;
    }

    public async Task<Vehicle?> UpdateAsync(Vehicle vehicle)
    {
        var existingVehicle = await _context.Vehicles.FindAsync(vehicle.VehicleId);

        if (existingVehicle is null)
        {
            return null;
        }

        existingVehicle.Brand = vehicle.Brand;
        existingVehicle.Model = vehicle.Model;
        existingVehicle.ManufactureYear = vehicle.ManufactureYear;
        existingVehicle.Mileage = vehicle.Mileage;
        existingVehicle.Price = vehicle.Price;
        existingVehicle.DealershipId = vehicle.DealershipId;

        await _context.SaveChangesAsync();

        return existingVehicle;
    }
}