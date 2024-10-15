namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Represents an application submitted by an abiturient for a specific speciality.
/// </summary>
public class Application : IEntity
{
    /// <summary>
    /// Unique identifier of the application.
    /// </summary>
    /// <example>101</example>
    public int Id { get; set; }

    /// <summary>
    /// Identifier of the speciality that the abiturient is applying for.
    /// </summary>
    /// <example>5001</example>
    public required int SpecialityId { get; set; }

    /// <summary>
    /// Identifier of the abiturient who submitted the application.
    /// </summary>
    /// <example>1</example>
    public required int AbiturientId { get; set; }

    /// <summary>
    /// Priority of the application. Lower values indicate higher priority.
    /// </summary>
    /// <example>1</example>
    public required int Priority { get; set; }
}
