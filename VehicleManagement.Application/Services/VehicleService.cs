using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;

    public VehicleService(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var vehicles = await _repository.GetAllAsync();

        return vehicles.Select(vehicle => new VehicleDto(
            vehicle.VehicleId,
            vehicle.Brand,
            vehicle.Model,
            vehicle.ManufactureYear,
            vehicle.Mileage,
            vehicle.Price,
            vehicle.DealershipId
        )).ToList();
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle is null)
        {
            return null;
        }

        return new VehicleDto(
            vehicle.VehicleId,
            vehicle.Brand,
            vehicle.Model,
            vehicle.ManufactureYear,
            vehicle.Mileage,
            vehicle.Price,
            vehicle.DealershipId
        );
    }
}