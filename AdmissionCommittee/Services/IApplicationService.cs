using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for application service
/// </summary>
public interface IApplicationService : IBaseService<Application, ApplicationDto, ApplicationCreateDto>
{
    /// <summary>
    /// Get ids of students who selected the speciality
    /// </summary>
    /// <param name="specialityId">A unique id of an speciality</param>
    /// <returns>A collection of Applications</returns>
    public Task<IEnumerable<ApplicationDto>> GetApplicationsBySpecialityIdAsync(int specialityId);
    /// <summary>
    /// Get all aplication with the priority level
    /// </summary>
    /// <param name="priority">number of priority</param>
    /// <returns>A collection of Applications</returns>
    public Task<IEnumerable<ApplicationDto>> GetApplicationsByPriorityAsync(int priority);
    /// <summary>
    /// Get all abiturient's applications by abiturient's id
    /// </summary>
    /// <param name="abiturientId"></param>
    /// <returns>A collection of applications</returns>
    public Task<IEnumerable<ApplicationDto>> GetApplicationsByAbiturientIdAsync(int abiturientId);
}