using Microsoft.EntityFrameworkCore;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Infrastructure.Persistence;

public class DealershipRepository : IDealershipRepository
{
    protected AppDbContext _context;

    public DealershipRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> DealershipExistsAsync(int id)
    {
        return _context.Dealerships.AnyAsync(dealership => dealership.DealershipId == id);
    }
}