using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Results;

namespace VehicleManagement.Application.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllAsync(int page, int pageSize, string? brand);
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto?> CreateAsync(CreateVehicleDto createVehicleDto);
    Task<bool> DeleteAsync(int id);
    Task<UpdateVehicleResult> UpdateAsync(int vehicleId, UpdateVehicleDto updateVehicleDto);
}