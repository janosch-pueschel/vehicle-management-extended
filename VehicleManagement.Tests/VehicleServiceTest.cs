using Microsoft.Extensions.Logging;
using NSubstitute;
using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Results;
using VehicleManagement.Application.Services;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Tests;

public class VehicleServiceTest
{
    [Fact]
    public async Task CreateAsync_WhenDealershipExists_CreatesAndReturnsVehicle()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var dealershipRepository = Substitute.For<IDealershipRepository>();
        var logger = Substitute.For<ILogger<VehicleService>>();

        var vehicleService = new VehicleService(vehicleRepository, dealershipRepository, logger);

        var createVehicleDto = new CreateVehicleDto(
            Brand: "Volvo",
            Model: "V60",
            ManufactureYear: 2016,
            Mileage: 75380,
            Price: 10600,
            DealershipId: 1,
            Color: "black"
        );

        dealershipRepository.DealershipExistsAsync(1).Returns(true);

        vehicleRepository
            .CreateAsync(Arg.Any<Vehicle>())
            .Returns(callInfo => callInfo.Arg<Vehicle>());

        var vehicle = await vehicleService.CreateAsync(createVehicleDto);

        Assert.NotNull(vehicle);
        Assert.Equal(createVehicleDto.Brand, vehicle.Brand);
        Assert.Equal(createVehicleDto.Model, vehicle.Model);
        Assert.Equal(createVehicleDto.ManufactureYear, vehicle.ManufactureYear);
        Assert.Equal(createVehicleDto.Mileage, vehicle.Mileage);
        Assert.Equal(createVehicleDto.Price, vehicle.Price);
        Assert.Equal(createVehicleDto.DealershipId, vehicle.DealershipId);
        Assert.Equal(createVehicleDto.Color, vehicle.Color);

        await vehicleRepository
            .Received(1)
            .CreateAsync(
                Arg.Is<Vehicle>(vehicle =>
                    vehicle.Brand == createVehicleDto.Brand &&
                    vehicle.Model == createVehicleDto.Model &&
                    vehicle.DealershipId == createVehicleDto.DealershipId
                    )
            );
    }

    [Fact]
    public async Task CreateAsync_WhenDealershipDoesNotExist_ReturnsNull()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var dealershipRepository = Substitute.For<IDealershipRepository>();
        var logger = Substitute.For<ILogger<VehicleService>>();

        var vehicleService = new VehicleService(vehicleRepository, dealershipRepository, logger);

        var createVehicleDto = new CreateVehicleDto(
            Brand: "Volvo",
            Model: "V60",
            ManufactureYear: 2016,
            Mileage: 75380,
            Price: 10600,
            DealershipId: 1,
            Color: "black"
        );

        dealershipRepository.DealershipExistsAsync(1).Returns(false);

        var vehicle = await vehicleService.CreateAsync(createVehicleDto);

        Assert.Null(vehicle);

        await vehicleRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Vehicle>());
    }

    [Fact]
    public async Task UpdateAsync_WhenVehicleDoesNotExist_ReturnsVehicleNotFound()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var dealershipRepository = Substitute.For<IDealershipRepository>();
        var logger = Substitute.For<ILogger<VehicleService>>();

        var vehicleService = new VehicleService(vehicleRepository, dealershipRepository, logger);

        int vehicleId = 1;
        var updateVehicleDto = new UpdateVehicleDto(
            Brand: "Volvo",
            Model: "V60",
            ManufactureYear: 2016,
            Mileage: 75380,
            Price: 10600,
            DealershipId: 1,
            Color: "black"
        );

        dealershipRepository.DealershipExistsAsync(1).Returns(true);

        vehicleRepository.UpdateAsync(Arg.Any<Vehicle>()).Returns((Vehicle?)null);

        var updateVehicleResult = await vehicleService.UpdateAsync(vehicleId, updateVehicleDto);

        Assert.Equal(UpdateVehicleStatus.VehicleNotFound, updateVehicleResult.Status);
        Assert.Null(updateVehicleResult.Vehicle);

        await vehicleRepository
            .Received(1)
            .UpdateAsync(Arg.Is<Vehicle>(vehicle => vehicle.VehicleId == vehicleId));
    }

    [Fact]
    public async Task UpdateAsync_WhenDealershipDoesNotExist_ReturnsDealershipNotFound()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var dealershipRepository = Substitute.For<IDealershipRepository>();
        var logger = Substitute.For<ILogger<VehicleService>>();

        var vehicleService = new VehicleService(vehicleRepository, dealershipRepository, logger);

        int vehicleId = 1;
        var updateVehicleDto = new UpdateVehicleDto(
            Brand: "Volvo",
            Model: "V60",
            ManufactureYear: 2016,
            Mileage: 75380,
            Price: 10600,
            DealershipId: 1,
            Color: "black"
        );

        dealershipRepository.DealershipExistsAsync(1).Returns(false);

        var updateVehicleResult = await vehicleService.UpdateAsync(vehicleId, updateVehicleDto);

        Assert.Equal(UpdateVehicleStatus.DealershipNotFound, updateVehicleResult.Status);
        Assert.Null(updateVehicleResult.Vehicle);

        await vehicleRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Vehicle>());
    }

    [Fact]
    public async Task UpdateAsync_WhenVehicleAndDealershipExist_ReturnsVehicleUpdated()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var dealershipRepository = Substitute.For<IDealershipRepository>();
        var logger = Substitute.For<ILogger<VehicleService>>();

        var vehicleService = new VehicleService(vehicleRepository, dealershipRepository, logger);

        int vehicleId = 1;
        var updateVehicleDto = new UpdateVehicleDto(
            Brand: "Volvo",
            Model: "V60",
            ManufactureYear: 2016,
            Mileage: 75380,
            Price: 10600,
            DealershipId: 1,
            Color: "black"
        );

        dealershipRepository.DealershipExistsAsync(1).Returns(true);

        vehicleRepository
            .UpdateAsync(Arg.Any<Vehicle>())
            .Returns(CallInfo => CallInfo.Arg<Vehicle>());

        var updateVehicleResult = await vehicleService.UpdateAsync(vehicleId, updateVehicleDto);

        Assert.Equal(UpdateVehicleStatus.Success, updateVehicleResult.Status);
        Assert.NotNull(updateVehicleResult.Vehicle);
        Assert.Equal(vehicleId, updateVehicleResult.Vehicle.VehicleId);
        Assert.Equal(updateVehicleDto.Brand, updateVehicleResult.Vehicle.Brand);
        Assert.Equal(updateVehicleDto.Model, updateVehicleResult.Vehicle.Model);
        Assert.Equal(updateVehicleDto.ManufactureYear, updateVehicleResult.Vehicle.ManufactureYear);
        Assert.Equal(updateVehicleDto.Mileage, updateVehicleResult.Vehicle.Mileage);
        Assert.Equal(updateVehicleDto.Price, updateVehicleResult.Vehicle.Price);
        Assert.Equal(updateVehicleDto.DealershipId, updateVehicleResult.Vehicle.DealershipId);
        Assert.Equal(updateVehicleDto.Color, updateVehicleResult.Vehicle.Color);

        await vehicleRepository
            .Received(1)
            .UpdateAsync(Arg.Is<Vehicle>(vehicle => vehicle.VehicleId == vehicleId));
    }
}
