using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;

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
    public async Task<ActionResult<List<VehicleDto>>> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto?>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Vehicle ID must be greater than zero.");
        }

        var vehicle = await _vehicleService.GetByIdAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto?>> Create(CreateVehicleDto createVehicleDto)
    {
        var vehicle = await _vehicleService.CreateAsync(createVehicleDto);

        if (vehicle is null)
        {
            return BadRequest("Dealership does not exist.");
        }


        return CreatedAtAction(
            nameof(GetById),
            new { id = vehicle.VehicleId },
            vehicle
        );
    }
}