namespace VehicleManagement.Domain.Entities;

public class Dealership
{
    public int DealershipId { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = [];
}