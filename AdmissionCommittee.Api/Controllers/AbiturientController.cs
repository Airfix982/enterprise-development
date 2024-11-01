using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Api.Controllers;

/// <summary>
/// Controller for managing abiturients.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AbiturientController
    (IAbiturientService abiturientService, ILogger<AbiturientController> logger) : ControllerBase
{
    private readonly IAbiturientService _abiturientService = abiturientService;
    private readonly ILogger<AbiturientController> _logger = logger;

    /// <summary>
    /// Retrieves all abiturients.
    /// </summary>
    /// <returns>A list of abiturients.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientDto>>> Get()
    {
        _logger.LogInformation("Retrieving all abiturients.");
        var abiturients = await _abiturientService.GetAllAsync();
        return Ok(abiturients);
    }

    /// <summary>
    /// Retrieves a specific abiturient by ID.
    /// </summary>
    /// <param name="id">The ID of the abiturient.</param>
    /// <returns>The requested abiturient.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AbiturientDto>> Get(int id)
    {
        _logger.LogInformation("Retrieving abiturient with ID: {id}", id);
        var abiturient = await _abiturientService.GetByIdAsync(id);
        return Ok(abiturient);
    }
    /// <summary>
    /// Adds a new abiturient.
    /// </summary>
    /// <param name="abiturient">The abiturient to add.</param>
    /// <returns>The newly created abiturient.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AbiturientCreateDto abiturient)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid abiturient data provided.");
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Adding new abiturient: {abiturient.Name}", abiturient.Name);
        var id = await _abiturientService.AddAsync(abiturient);
        return CreatedAtAction(nameof(Get), new { id }, abiturient);
    }

    /// <summary>
    /// Updates an existing abiturient.
    /// </summary>
    /// <param name="id">The ID of the abiturient to update.</param>
    /// <param name="abiturient">The updated abiturient data.</param>
    /// <returns>No content if the update is successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, [FromBody] AbiturientCreateDto abiturient)
    {
        _logger.LogInformation("Updating abiturient with ID: {id}", id);
        await _abiturientService.UpdateAsync(id, abiturient);
        return NoContent();
    }

    /// <summary>
    /// Deletes an abiturient by ID.
    /// </summary>
    /// <param name="id">The ID of the abiturient to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting abiturient with ID: {id}", id);
        await _abiturientService.DeleteAsync(id);
        return NoContent();
    }
    /// <summary>
    /// Retrieves all abiturients from a specific city.
    /// </summary>
    /// <param name="city">The city name to filter abiturients by.</param>
    /// <returns>A list of abiturients from the specified city.</returns>
    [HttpGet("city/{city}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientDto>>> GetByCity(string city)
    {
        _logger.LogInformation("Retrieving abiturients from city: {city}", city);
        var abiturients = await _abiturientService.GetAbiturientsByCityAsync(city);
        return Ok(abiturients);
    }

    /// <summary>
    /// Retrieves abiturients older than a specified age.
    /// </summary>
    /// <param name="age">The minimum age of the abiturients to retrieve.</param>
    /// <returns>A list of abiturients older than the specified age.</returns>
    [HttpGet("olderthan/{age:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientDto>>> GetOlderThan(int age)
    {
        _logger.LogInformation("Retrieving abiturients older than {age} years.", age);
        var abiturients = await _abiturientService.GetAbiturientsOlderThanAsync(age);
        return Ok(abiturients);
    }

    /// <summary>
    /// Retrieves abiturients by speciality, ordered by their exam results.
    /// </summary>
    /// <param name="specialityId">The ID of the speciality to filter abiturients by.</param>
    /// <returns>A list of abiturients ordered by their exam results.</returns>
    [HttpGet("speciality/{specialityId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientDto>>> GetBySpeciality(int specialityId)
    {
        _logger.LogInformation("Retrieving abiturients ordered by exam results for speciality {specialityId}.", specialityId);
        var abiturients = await _abiturientService.GetAbiturientBySpecialityOrderedByRatesAsync(specialityId);
        return Ok(abiturients);
    }

    /// <summary>
    /// Retrieves the count of abiturients who chose each speciality as their first priority.
    /// </summary>
    /// <returns>A list of specialities with the count of abiturients who chose them as their first priority.</returns>
    [HttpGet("firstpriority")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SpecialitiesCountAsFavoriteDto>>> GetFirstPrioritySpecialitiesCount()
    {
        _logger.LogInformation("Retrieving abiturients count by first priority specialities.");
        var result = await _abiturientService.GetAbiturientsCountByFirstPrioritySpecialitiesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the top-rated abiturients based on their exam scores.
    /// </summary>
    /// <param name="count">The number of top-rated abiturients to retrieve.</param>
    /// <returns>A list of top-rated abiturients.</returns>
    [HttpGet("toprated/{count:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientWithExamScoresDto>>> GetTopRatedAbiturients(int count)
    {
        _logger.LogInformation("Retrieving top rated abiturients in count of: {count}", count);
        var abiturients = await _abiturientService.GetTopRatedAbiturientsAsync(count);
        return Ok(abiturients);
    }

    /// <summary>
    /// Retrieves abiturients with the highest exam scores and their favorite specialities.
    /// </summary>
    /// <returns>A list of abiturients with the highest exam scores and their favorite specialities.</returns>
    [HttpGet("maxratedfavoritespeciality")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AbiturientMaxRateDto>>> GetMaxRatedAbiturientsWithFavoriteSpeciality()
    {
        _logger.LogInformation("Retrieving abiturients with maximum exam results and their favorite speciality.");
        var result = await _abiturientService.GetMaxRatedAbiturientsWithFavoriteSpecialityAsync();
        return Ok(result);
    }
}
