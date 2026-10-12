using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Application.Interfaces;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllAsync(int page, int pageSize, string? brand);
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle> CreateAsync(Vehicle vehicle);
    Task<bool> DeleteAsync(int id);
    Task<Vehicle?> UpdateAsync(Vehicle vehicle);
}