using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Services;
using VehicleManagement.Domain.Entities;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
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