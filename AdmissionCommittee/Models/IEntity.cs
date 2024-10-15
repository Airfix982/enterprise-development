namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Base interface for models
/// </summary>
public interface IEntity
{
    /// <summary>
    /// An unique identificator
    /// </summary>
    int Id { get; set; }
}
