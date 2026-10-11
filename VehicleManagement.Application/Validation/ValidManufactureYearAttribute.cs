using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Application.Validation;


public class ValidManufactureYearAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? manufactureYear, ValidationContext validationContext)
    {
        var currentYear = DateTime.UtcNow.Year;

        if (manufactureYear is int year && year >= 1886 && year <= currentYear)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid manufacture year.");
    }
}