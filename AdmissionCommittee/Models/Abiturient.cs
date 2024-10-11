namespace AdmissionCommittee.Domain.Models;
/// <summary>
/// Abiturient who sends documents to enter the university
/// </summary>
public class Abiturient : IEntity
{
    /// <summary>
    /// Unique identifier of the abiturient.
    /// </summary>
    /// <example>101</example>
    public required int Id { get; set; }
    /// <summary>
    /// Abiturient's name
    /// </summary>
    /// <example>Ivan</example>
    public required string Name { get; set; }
    /// <summary>
    /// Abiturient's name
    /// </summary>
    /// <example>Ivanov</example>
    public required string LastName { get; set; }
    /// <summary>
    /// Birthday date of an abiturient
    /// </summary>
    /// <example>1900-02-30</example>
    public required DateTime BirthdayDate { get; set; }
    /// <summary>
    /// Abiturient's country where he is from
    /// </summary>
    /// <example>Mauritania</example>
    public required string Country { get; set; }
    /// <summary>
    /// Abiturient's city where he is from
    /// </summary>
    /// <example>Jezza</example>
    public required string City { get; set; }
}
