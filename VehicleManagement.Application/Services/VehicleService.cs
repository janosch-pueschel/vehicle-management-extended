using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Results;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDealershipRepository _dealershipRepository;


    public VehicleService(IVehicleRepository vehicleRepository, IDealershipRepository dealershipRepository)
    {
        _vehicleRepository = vehicleRepository;
        _dealershipRepository = dealershipRepository;
    }

    public async Task<List<VehicleDto>> GetAllAsync(int page, int pageSize, string? brand)
    {
        var vehicles = await _vehicleRepository.GetAllAsync(page, pageSize, brand);

        return vehicles.Select(vehicle => ToDto(vehicle)).ToList();
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);

        if (vehicle is null)
        {
            return null;
        }

        return ToDto(vehicle);
    }

    public async Task<VehicleDto?> CreateAsync(CreateVehicleDto createVehicleDto)
    {
        var dealershipExists = await _dealershipRepository.DealershipExistsAsync(createVehicleDto.DealershipId);

        if (!dealershipExists)
        {
            return null;
        }

        var newVehicle = new Vehicle
        {
            Brand = createVehicleDto.Brand,
            Model = createVehicleDto.Model,
            ManufactureYear = createVehicleDto.ManufactureYear,
            Mileage = createVehicleDto.Mileage,
            Price = createVehicleDto.Price,
            DealershipId = createVehicleDto.DealershipId,
            Color = createVehicleDto.Color
        };

        var vehicle = await _vehicleRepository.CreateAsync(newVehicle);

        return ToDto(vehicle);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _vehicleRepository.DeleteAsync(id);
    }

    public async Task<UpdateVehicleResult> UpdateAsync(int vehicleId, UpdateVehicleDto updateVehicleDto)
    {
        var dealershipExists = await _dealershipRepository.DealershipExistsAsync(updateVehicleDto.DealershipId);

        if (!dealershipExists)
        {
            return new UpdateVehicleResult(UpdateVehicleStatus.DealershipNotFound);
        }

        var updatedVehicle = new Vehicle
        {
            VehicleId = vehicleId,
            Brand = updateVehicleDto.Brand,
            Model = updateVehicleDto.Model,
            ManufactureYear = updateVehicleDto.ManufactureYear,
            Mileage = updateVehicleDto.Mileage,
            Price = updateVehicleDto.Price,
            DealershipId = updateVehicleDto.DealershipId,
            Color = updateVehicleDto.Color
        };

        var vehicle = await _vehicleRepository.UpdateAsync(updatedVehicle);

        if (vehicle is null)
        {
            return new UpdateVehicleResult(UpdateVehicleStatus.VehicleNotFound);
        }

        var storedVehicle = ToDto(vehicle);

        return new UpdateVehicleResult(UpdateVehicleStatus.Success, storedVehicle);
    }

    private static VehicleDto ToDto(Vehicle vehicle)
    {
        return new VehicleDto(
            vehicle.VehicleId,
            vehicle.Brand,
            vehicle.Model,
            vehicle.ManufactureYear,
            vehicle.Mileage,
            vehicle.Price,
            vehicle.DealershipId,
            vehicle.Color
        );
    }
}