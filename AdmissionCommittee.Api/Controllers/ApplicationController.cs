using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Api.Controllers;

/// <summary>
/// Controller for managing applications.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ApplicationController
    (IApplicationService applicationService, ILogger<ApplicationController> logger) : ControllerBase
{
    private readonly IApplicationService _applicationService = applicationService;
    private readonly ILogger<ApplicationController> _logger = logger;

    /// <summary>
    /// Retrieves all applications.
    /// </summary>
    /// <returns>A list of applications.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ApplicationDto>> Get()
    {
        _logger.LogInformation("Retrieving all applications.");
        var applications = _applicationService.GetAll();
        return Ok(applications);
    }

    /// <summary>
    /// Retrieves a specific application by ID.
    /// </summary>
    /// <param name="id">The ID of the application.</param>
    /// <returns>The requested application.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ApplicationDto> Get(int id)
    {
        _logger.LogInformation("Retrieving application with ID: {id}", id);
        var application = _applicationService.GetById(id);
        return Ok(application);
    }

    /// <summary>
    /// Adds a new application.
    /// </summary>
    /// <param name="application">The application to add.</param>
    /// <returns>The newly created application.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add([FromBody]ApplicationCreateDto application)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid application data provided.");
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Adding new application for Abiturient ID: {application.AbiturientId}", application.AbiturientId);
        var id = _applicationService.Add(application);
        return CreatedAtAction(nameof(Get), new { id }, application);

    }

    /// <summary>
    /// Updates an existing application.
    /// </summary>
    /// <param name="id">The ID of the application to update.</param>
    /// <param name="application">The updated application data.</param>
    /// <returns>No content if the update is successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(int id, [FromBody]ApplicationCreateDto application)
    {
        _logger.LogInformation("Updating application with ID: {id}", id);
        _applicationService.Update(id, application);
        return NoContent();
    }

    /// <summary>
    /// Deletes an application by ID.
    /// </summary>
    /// <param name="id">The ID of the application to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting application with ID: {id}", id);
        _applicationService.Delete(id);
        return NoContent();
    }
}
