using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;
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

    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();

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
        var vehicle = await _vehicleRepository.GetByIdAsync(id);

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
            DealershipId = createVehicleDto.DealershipId
        };

        var vehicle = await _vehicleRepository.CreateAsync(newVehicle);

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