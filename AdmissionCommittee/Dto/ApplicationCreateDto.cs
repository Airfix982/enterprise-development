using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdmissionCommittee.Domain.Dto;
/// <summary>
/// Dto for an application submitted by an abiturient adding.
/// </summary>
public class ApplicationCreateDto : IDto
{
    /// <summary>
    /// Id of the speciality applied for.
    /// </summary>
    [JsonPropertyName("speciality_id")]
    [Required]
    public required int SpecialityId { get; set; }
    /// <summary>
    /// Id of the abiturient who submitted the application.
    /// </summary>
    [JsonPropertyName("abiturient_id")]
    [Required]
    public required int AbiturientId { get; set; }
    /// <summary>
    /// Priority of the application. Lower values indicate higher priority.
    /// </summary>
    [JsonPropertyName("priority")]
    [Required]
    [Range(1, 3, ErrorMessage = "Priority must be between 1 `n` 3.")]
    public required int Priority { get; set; }
}
