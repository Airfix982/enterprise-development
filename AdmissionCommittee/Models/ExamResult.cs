using System.ComponentModel.DataAnnotations;

namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Result of an exam
/// </summary>
public class ExamResult : IEntity
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
    /// <summary>
    /// Id of an abiturient
    /// </summary>
    /// <example>1</example>
    public required int AbiturientId { get; set; }
    /// <summary>
    /// Name of an exam
    /// </summary>
    /// <example>Mathematics</example>
    [MaxLength(50)]
    public required string ExamName { get; set; }
    /// <summary>
    /// Abiturient's result by an exam
    /// </summary>
    /// <example>87</example>
    public required int Result { get; set; }
}

