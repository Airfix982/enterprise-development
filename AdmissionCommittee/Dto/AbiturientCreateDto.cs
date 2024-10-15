using AdmissionCommittee.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Dto;
/// <summary>
/// Dto for an abiturient adding.
/// </summary>
public class AbiturientCreateDto : IDto
{
    /// <summary>
    /// First name of the abiturient.
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    /// <summary>
    /// Last name of the abiturient.
    /// </summary>
    [Required]
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }
    /// <summary>
    /// Date of birth of the abiturient.
    /// </summary>
    [Required]
    [JsonPropertyName("birthday_date")]
    [BirthdateValidation]
    public required DateTime BirthdayDate { get; set; }
    /// <summary>
    /// Country of the abiturient.
    /// </summary>
    [Required]
    [JsonPropertyName("country")]
    public required string Country { get; set; }
    /// <summary>
    /// City of the abiturient.
    /// </summary>
    [Required]
    [JsonPropertyName("city")]
    public required string City { get; set; }
}
