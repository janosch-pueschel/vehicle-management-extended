using System.ComponentModel.DataAnnotations;
using VehicleManagement.Application.Validation;

namespace VehicleManagement.Application.DTOs;

public record UpdateVehicleDto(
    [Required]
    [RegularExpression(@".*\S.*")]
    string Brand,

    [Required]
    [RegularExpression(@".*\S.*")]
    string Model,

    [ValidManufactureYear]
    int ManufactureYear,

    [Range(0, int.MaxValue)]
    int Mileage,

    [Range(typeof(decimal), "0.01", "999999999", ParseLimitsInInvariantCulture = true)]
    decimal Price,

    [Range(1, int.MaxValue)]
    int DealershipId,

    [RegularExpression(@".*\S.*")]
    string Color
);