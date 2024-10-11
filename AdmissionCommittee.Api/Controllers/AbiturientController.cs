using Microsoft.AspNetCore.Mvc;
using AdmissionCommittee.Domain.Services;
using AdmissionCommittee.Domain.Dto;

namespace AdmissionCommittee.Api.Controllers
{
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
        public ActionResult<IEnumerable<AbiturientDto>> GetAll()
        {
            _logger.LogInformation("Retrieving all abiturients.");
            var abiturients = _abiturientService.GetAll();
            return Ok(abiturients);
        }

        /// <summary>
        /// Retrieves a specific abiturient by ID.
        /// </summary>
        /// <param name="id">The ID of the abiturient.</param>
        /// <returns>The requested abiturient.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AbiturientDto> GetById(int id)
        {
            _logger.LogInformation($"Retrieving abiturient with ID: {id}");
            var abiturient = _abiturientService.GetById(id);
            if (abiturient == null)
            {
                _logger.LogWarning($"Abiturient with ID {id} not found.");
                return NotFound();
            }
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
        public IActionResult Add(AbiturientDto abiturient)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid abiturient data provided.");
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Adding new abiturient: {abiturient.Name}");
            _abiturientService.Add(abiturient);
            return CreatedAtAction(nameof(GetById), new { id = abiturient.Id }, abiturient);
        }

        /// <summary>
        /// Updates an existing abiturient.
        /// </summary>
        /// <param name="id">The ID of the abiturient to update.</param>
        /// <param name="abiturient">The updated abiturient data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, AbiturientDto abiturient)
        {
            if (id != abiturient.Id)
            {
                _logger.LogWarning($"Update failed: ID mismatch. URL ID: {id}, Abiturient ID: {abiturient.Id}");
                return BadRequest();
            }

            var existingAbiturient = _abiturientService.GetById(id);
            if (existingAbiturient == null)
            {
                _logger.LogWarning($"Abiturient with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updating abiturient with ID: {id}");
            _abiturientService.Update(abiturient);
            return NoContent();
        }

        /// <summary>
        /// Deletes an abiturient by ID.
        /// </summary>
        /// <param name="id">The ID of the abiturient to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existingAbiturient = _abiturientService.GetById(id);
            if (existingAbiturient == null)
            {
                _logger.LogWarning($"Abiturient with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleting abiturient with ID: {id}");
            _abiturientService.Delete(id);
            return NoContent();
        }
    }
}
