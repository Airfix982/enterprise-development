using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for application service
/// </summary>
public interface IApplicationService : IBaseService<Application, ApplicationDto>
{
    /// <summary>
    /// Get ids of students who selected the speciality
    /// </summary>
    /// <param name="specialityId">A unique id of an speciality</param>
    /// <returns>A collection of Applications</returns>
    public IEnumerable<Application> GetApplicationsBySpecialityId(int specialityId);
    /// <summary>
    /// Get all aplication with the priority level
    /// </summary>
    /// <param name="priority">number of priority</param>
    /// <returns>A collection of Applications</returns>
    public IEnumerable<Application> GetApplicationsByPriority(int priority);
    /// <summary>
    /// Get all abiturient's applications by abiturient's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A collection of applications</returns>
    public IEnumerable<Application> GetApplicationsByAbiturientId(int abiturientId);
}