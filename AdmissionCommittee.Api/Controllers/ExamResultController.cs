using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ExamResultDto>>> Get()
    {
        _logger.LogInformation("Retrieving all exam results.");
        var examResults = await _examResultService.GetAllAsync();
        return Ok(examResults);
    }

    /// <summary>
    /// Retrieves a specific exam result by ID.
    /// </summary>
    /// <param name="id">The ID of the exam result.</param>
    /// <returns>The requested exam result.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ExamResultDto>> Get(int id)
    {
        _logger.LogInformation("Retrieving exam result with ID: {id}", id);
        var examResult = await _examResultService.GetByIdAsync(id);
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
    public async Task<IActionResult> Add([FromBody] ExamResultCreateDto examResult)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid exam result data provided.");
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Adding new exam result for Abiturient ID: {examResult.AbiturientId}", examResult.AbiturientId);
        var id = await _examResultService.AddAsync(examResult);
        return CreatedAtAction(nameof(Get), new { id }, examResult);
    }

    /// <summary>
    /// Updates an existing exam result.
    /// </summary>
    /// <param name="id">The ID of the exam result to update.</param>
    /// <param name="examResult">The updated exam result data.</param>
    /// <returns>No content if the update is successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, [FromBody] ExamResultCreateDto examResult)
    {
        _logger.LogInformation("Updating exam result with ID: {id}", id);
        await _examResultService.UpdateAsync(id, examResult);
        return NoContent();
    }

    /// <summary>
    /// Deletes an exam result by ID.
    /// </summary>
    /// <param name="id">The ID of the exam result to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting exam result with ID: {id}", id);
        await _examResultService.DeleteAsync(id);
        return NoContent();
    }
}
