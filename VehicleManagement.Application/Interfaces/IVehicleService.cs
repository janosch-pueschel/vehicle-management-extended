using VehicleManagement.Application.DTOs;

namespace VehicleManagement.Application.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllAsync();
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto?> CreateAsync(CreateVehicleDto createVehicleDto);
}