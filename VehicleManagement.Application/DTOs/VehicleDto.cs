namespace VehicleManagement.Application.DTOs;

public record VehicleDto(
    int VehicleId,
    string Brand,
    string Model,
    int ManufactureYear,
    int Mileage,
    decimal Price,
    int DealershipId,
    string? Color
);