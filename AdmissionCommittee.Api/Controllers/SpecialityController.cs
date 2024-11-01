using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Api.Controllers;

/// <summary>
/// Controller for managing specialities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SpecialityController
    (ISpecialityService specialityService, ILogger<SpecialityController> logger) : ControllerBase
{
    private readonly ISpecialityService _specialityService = specialityService;
    private readonly ILogger<SpecialityController> _logger = logger;

    /// <summary>
    /// Retrieves all specialities.
    /// </summary>
    /// <returns>A list of specialities.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SpecialityDto>>> Get()
    {
        _logger.LogInformation("Retrieving all specialities.");
        var specialities = await _specialityService.GetAllAsync();
        return Ok(specialities);
    }

    /// <summary>
    /// Retrieves a specific speciality by ID.
    /// </summary>
    /// <param name="id">The ID of the speciality.</param>
    /// <returns>The requested speciality.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SpecialityDto>> Get(int id)
    {
        _logger.LogInformation("Retrieving speciality with ID: {id}", id);
        var speciality = await _specialityService.GetByIdAsync(id);
        return Ok(speciality);
    }

    /// <summary>
    /// Adds a new speciality.
    /// </summary>
    /// <param name="speciality">The speciality to add.</param>
    /// <returns>The newly created speciality.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] SpecialityCreateDto speciality)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid speciality data provided.");
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Adding new speciality: {speciality.Name}", speciality.Name);
        var id = await _specialityService.AddAsync(speciality);
        return CreatedAtAction(nameof(Get), new { id }, speciality);
    }

    /// <summary>
    /// Updates an existing speciality.
    /// </summary>
    /// <param name="id">The ID of the speciality to update.</param>
    /// <param name="speciality">The updated speciality data.</param>
    /// <returns>No content if the update is successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, [FromBody] SpecialityCreateDto speciality)
    {
        _logger.LogInformation("Updating speciality with ID: {id}", id);
        await _specialityService.UpdateAsync(id, speciality);
        return NoContent();
    }

    /// <summary>
    /// Deletes a speciality by ID.
    /// </summary>
    /// <param name="id">The ID of the speciality to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting speciality with ID: {id}", id);
        await _specialityService.DeleteAsync(id);
        return NoContent();
    }
}
