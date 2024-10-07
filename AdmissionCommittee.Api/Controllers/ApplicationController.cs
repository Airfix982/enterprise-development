using Microsoft.AspNetCore.Mvc;
using AdmissionCommittee.Domain.Services;
using AdmissionCommittee.Domain.Dto;

namespace AdmissionCommittee.Api.Controllers
{
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
        public ActionResult<IEnumerable<ApplicationDto>> GetAll()
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationDto> GetById(int id)
        {
            _logger.LogInformation($"Retrieving application with ID: {id}");
            var application = _applicationService.GetById(id);
            if (application == null)
            {
                _logger.LogWarning($"Application with ID {id} not found.");
                return NotFound();
            }
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
        public IActionResult Add(ApplicationDto application)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid application data provided.");
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Adding new application for Abiturient ID: {application.AbiturientId}");
            _applicationService.Add(application);
            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
        }

        /// <summary>
        /// Updates an existing application.
        /// </summary>
        /// <param name="id">The ID of the application to update.</param>
        /// <param name="application">The updated application data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, ApplicationDto application)
        {
            if (id != application.Id)
            {
                _logger.LogWarning($"Update failed: ID mismatch. URL ID: {id}, Application ID: {application.Id}");
                return BadRequest();
            }

            var existingApplication = _applicationService.GetById(id);
            if (existingApplication == null)
            {
                _logger.LogWarning($"Application with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updating application with ID: {id}");
            _applicationService.Update(application);
            return NoContent();
        }

        /// <summary>
        /// Deletes an application by ID.
        /// </summary>
        /// <param name="id">The ID of the application to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existingApplication = _applicationService.GetById(id);
            if (existingApplication == null)
            {
                _logger.LogWarning($"Application with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleting application with ID: {id}");
            _applicationService.Delete(id);
            return NoContent();
        }
    }
}
