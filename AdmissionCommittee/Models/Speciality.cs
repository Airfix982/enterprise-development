namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Represents a speciality offered by a university.
/// </summary>
public class Speciality : IEntity
{
    /// <summary>
    /// Unique identifier of the speciality.
    /// </summary>
    /// <example>3001</example>
    public int Id { get; set; }

    /// <summary>
    /// Speciality code or number used to identify the speciality.
    /// </summary>
    /// <example>105003D</example>
    public required string Number { get; set; }

    /// <summary>
    /// Name of the speciality.
    /// </summary>
    /// <example>Cyber Security</example>
    public required string Name { get; set; }

    /// <summary>
    /// The faculty or department that offers this speciality.
    /// </summary>
    /// <example>Informatics</example>
    public required string Facility { get; set; }
}
