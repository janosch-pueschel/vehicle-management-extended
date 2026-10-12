using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Application.DTOs;
using VehicleManagement.Application.Interfaces;
using VehicleManagement.Application.Results;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Returns a paginated list of vehicles, optionally filtered by brand.
    /// </summary>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Number of vehicles per page (default: 20, maximum: 100).</param>
    /// <param name="brand">Optional brand filter.</param>
    /// <returns>A list of vehicles matching the specified criteria.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<VehicleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<VehicleDto>>> GetAll(
        [FromQuery(Name = "page")] int page = 1,
        [FromQuery(Name = "pageSize")] int pageSize = 20,
        [FromQuery(Name = "brand")] string? brand = null
        )
    {
        if (pageSize > 100 || pageSize < 1)
        {
            return BadRequest("Page Size must be between 1 and 100.");
        }

        if (page < 1)
        {
            return BadRequest("Page must be greater than 0.");
        }

        var vehicles = await _vehicleService.GetAllAsync(page, pageSize, brand);

        return Ok(vehicles);
    }


    /// <summary>
    /// Returns a vehicle by its ID.
    /// </summary>
    /// <param name="id">The vehicle ID.</param>
    /// <returns>The requested vehicle.</returns>
    /// <response code="400">Invalid vehicle ID.</response>
    /// <response code="404">Vehicle not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="createVehicleDto">
    /// The vehicle data used to create a new vehicle.
    /// </param>
    /// <returns>The newly created vehicle.</returns>
    /// <response code="201">Vehicle successfully created.</response>
    /// <response code="400">Invalid vehicle data or dealership.</response>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Deletes a vehicle by its ID.
    /// </summary>
    /// <param name="id">The ID of the vehicle to delete.</param>
    /// <response code="204">Vehicle successfully deleted.</response>
    /// <response code="400">Invalid vehicle ID.</response>
    /// <response code="404">Vehicle not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Vehicle ID must be greater than zero.");
        }

        var vehicleDeleted = await _vehicleService.DeleteAsync(id);

        if (!vehicleDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Updates an existing vehicle.
    /// </summary>
    /// <param name="id">The ID of the vehicle to update.</param>
    /// <param name="updateVehicleDto">The updated vehicle data.</param>
    /// <returns>The updated vehicle.</returns>
    /// <response code="200">Vehicle successfully updated.</response>
    /// <response code="400">Invalid vehicle data, invalid ID, or dealership does not exist.</response>
    /// <response code="404">Vehicle not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleDto?>> Update(int id, UpdateVehicleDto updateVehicleDto)
    {
        if (id <= 0)
        {
            return BadRequest("Vehicle ID must be greater than zero.");
        }

        var updateVehicleResult = await _vehicleService.UpdateAsync(id, updateVehicleDto);

        if (updateVehicleResult.Status is UpdateVehicleStatus.DealershipNotFound)
        {
            return BadRequest("Dealership does not exist.");
        }

        if (updateVehicleResult.Status is UpdateVehicleStatus.VehicleNotFound)
        {
            return NotFound();
        }


        return Ok(updateVehicleResult.Vehicle);
    }
}