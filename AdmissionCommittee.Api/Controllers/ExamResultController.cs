﻿using Microsoft.AspNetCore.Mvc;
using AdmissionCommittee.Domain.Services;
using AdmissionCommittee.Domain.Dto;

namespace AdmissionCommittee.Api.Controllers
{
    /// <summary>
    /// Controller for managing exam results.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExamResultController
        (IExamResultService examResultService, ILogger<ExamResultController> logger) : ControllerBase
    {
        private readonly IExamResultService _examResultService = examResultService;
        private readonly ILogger<ExamResultController> _logger = logger;

        /// <summary>
        /// Retrieves all exam results.
        /// </summary>
        /// <returns>A list of exam results.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ExamResultDto>> GetAll()
        {
            _logger.LogInformation("Retrieving all exam results.");
            var examResults = _examResultService.GetAll();
            return Ok(examResults);
        }

        /// <summary>
        /// Retrieves a specific exam result by ID.
        /// </summary>
        /// <param name="id">The ID of the exam result.</param>
        /// <returns>The requested exam result.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ExamResultDto> GetById(int id)
        {
            _logger.LogInformation($"Retrieving exam result with ID: {id}");
            var examResult = _examResultService.GetById(id);
            if (examResult == null)
            {
                _logger.LogWarning($"Exam result with ID {id} not found.");
                return NotFound();
            }
            return Ok(examResult);
        }

        /// <summary>
        /// Adds a new exam result.
        /// </summary>
        /// <param name="examResult">The exam result to add.</param>
        /// <returns>The newly created exam result.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add(ExamResultDto examResult)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid exam result data provided.");
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Adding new exam result for Abiturient ID: {examResult.AbiturientId}");
            _examResultService.Add(examResult);
            return CreatedAtAction(nameof(GetById), new { id = examResult.Id }, examResult);
        }

        /// <summary>
        /// Updates an existing exam result.
        /// </summary>
        /// <param name="id">The ID of the exam result to update.</param>
        /// <param name="examResult">The updated exam result data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, ExamResultDto examResult)
        {
            if (id != examResult.Id)
            {
                _logger.LogWarning($"Update failed: ID mismatch. URL ID: {id}, ExamResult ID: {examResult.Id}");
                return BadRequest();
            }

            var existingExamResult = _examResultService.GetById(id);
            if (existingExamResult == null)
            {
                _logger.LogWarning($"ExamResult with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updating exam result with ID: {id}");
            _examResultService.Update(examResult);
            return NoContent();
        }

        /// <summary>
        /// Deletes an exam result by ID.
        /// </summary>
        /// <param name="id">The ID of the exam result to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existingExamResult = _examResultService.GetById(id);
            if (existingExamResult == null)
            {
                _logger.LogWarning($"ExamResult with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleting exam result with ID: {id}");
            _examResultService.Delete(id);
            return NoContent();
        }
    }
}
