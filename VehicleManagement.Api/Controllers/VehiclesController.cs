using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Application.Services;
using VehicleManagement.Domain.Entities;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleService _vehicleService;

    public VehiclesController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Vehicle>>> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();

        return Ok(vehicles);
    }
}