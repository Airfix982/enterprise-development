using Microsoft.AspNetCore.Mvc;
using AdmissionCommittee.Domain.Services;
using AdmissionCommittee.Domain.Dto;

namespace AdmissionCommittee.Api.Controllers
{
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
        public ActionResult<IEnumerable<SpecialityDto>> GetAll()
        {
            _logger.LogInformation("Retrieving all specialities.");
            var specialities = _specialityService.GetAll();
            return Ok(specialities);
        }

        /// <summary>
        /// Retrieves a specific speciality by ID.
        /// </summary>
        /// <param name="id">The ID of the speciality.</param>
        /// <returns>The requested speciality.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SpecialityDto> GetById(int id)
        {
            _logger.LogInformation($"Retrieving speciality with ID: {id}");
            var speciality = _specialityService.GetById(id);
            if (speciality == null)
            {
                _logger.LogWarning($"Speciality with ID {id} not found.");
                return NotFound();
            }
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
        public IActionResult Add(SpecialityDto speciality)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid speciality data provided.");
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Adding new speciality: {speciality.Name}");
            _specialityService.Add(speciality);
            return CreatedAtAction(nameof(GetById), new { id = speciality.Id }, speciality);
        }

        /// <summary>
        /// Updates an existing speciality.
        /// </summary>
        /// <param name="id">The ID of the speciality to update.</param>
        /// <param name="speciality">The updated speciality data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, SpecialityDto speciality)
        {
            if (id != speciality.Id)
            {
                _logger.LogWarning($"Update failed: ID mismatch. URL ID: {id}, Speciality ID: {speciality.Id}");
                return BadRequest();
            }

            var existingSpeciality = _specialityService.GetById(id);
            if (existingSpeciality == null)
            {
                _logger.LogWarning($"Speciality with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updating speciality with ID: {id}");
            _specialityService.Update(speciality);
            return NoContent();
        }

        /// <summary>
        /// Deletes a speciality by ID.
        /// </summary>
        /// <param name="id">The ID of the speciality to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var speciality = _specialityService.GetById(id);
            if (speciality == null)
            {
                _logger.LogWarning($"Speciality with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleting speciality with ID: {id}");
            _specialityService.Delete(id);
            return NoContent();
        }
    }
}
