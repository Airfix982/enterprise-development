﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Dto;
/// <summary>
/// Dto for speciality adding
/// </summary>
public class SpecialityCreateDto : IDto
{
    /// <summary>
    /// Name of speciality
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    /// <summary>
    /// Number of speciality
    /// </summary>
    [Required]
    [JsonPropertyName("number")]
    public required string Number { get; set; }
    /// <summary>
    /// Facility of speciality
    /// </summary>
    [Required]
    [JsonPropertyName("facility")]
    public required string Facility { get; set; }
}