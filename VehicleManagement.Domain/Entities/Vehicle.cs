namespace VehicleManagement.Domain.Entities;

public class Vehicle
{
    public int VehicleId { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int ManufactureYear { get; set; }
    public required int Mileage { get; set; }
    public required decimal Price { get; set; }
    public int DealershipId { get; set; }
    public Dealership Dealership { get; set; } = null!;
}