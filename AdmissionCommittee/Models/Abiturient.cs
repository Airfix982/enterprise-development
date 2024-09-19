﻿namespace AdmissionCommittee.Domain.Models;
/// <summary>
/// Abiturient who sends documents to enter the university
/// </summary>
public class Abiturient
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int Id { get; set; }
    /// <summary>
    /// Abiturient's name
    /// </summary>
    /// <example>Ivan</example>
    public required string Name { get; set; }
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
