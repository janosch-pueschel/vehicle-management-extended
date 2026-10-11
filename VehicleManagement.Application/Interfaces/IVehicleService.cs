using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Application.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAllAsync();
}