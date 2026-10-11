namespace VehicleManagement.Application.Interfaces;

public interface IDealershipRepository
{
    Task<bool> DealershipExistsAsync(int id);
}