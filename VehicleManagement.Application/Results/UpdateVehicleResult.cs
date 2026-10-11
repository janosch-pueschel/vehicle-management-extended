using VehicleManagement.Application.DTOs;

namespace VehicleManagement.Application.Results;

public enum UpdateVehicleStatus
{
    Success,
    VehicleNotFound,
    DealershipNotFound
}

public record UpdateVehicleResult(
    UpdateVehicleStatus Status,
    VehicleDto? Vehicle = null
);