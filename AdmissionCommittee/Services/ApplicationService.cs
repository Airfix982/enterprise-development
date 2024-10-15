using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// Service for managing applications submitted by abiturients for various specialities.
/// This service provides business logic for managing the lifecycle of applications,
/// such as adding, updating, retrieving, and deleting applications. It interacts with the
/// application repository to perform CRUD operations and includes filtering methods based on
/// speciality, priority, and abiturient.
/// </summary>
public class ApplicationService(
    IAbiturientRepository abiturientRepository,
    ISpecialityRepository specialityRepository,
    IApplicationRepository applicationRepository) : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository = applicationRepository;
    private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    /// <inheritdoc />
    public IEnumerable<Application> GetAll() => _applicationRepository.GetAll();
    /// <inheritdoc />
    public Application GetById(int id)
    {
        var application = _applicationRepository.GetById(id);
        if (application == null)
            throw new KeyNotFoundException("Application not found");
        return application;
    }
    /// <inheritdoc />
    public int Add(ApplicationCreateDto applicationDto)
    {
        if (_abiturientRepository.GetById(applicationDto.AbiturientId) == null)
            throw new InvalidOperationException("Application to not existing abiturient");
        var abiturientApplicationsCount = GetApplicationsByAbiturientId(applicationDto.AbiturientId).Count();
        var maxApplicationsCount = 3;
        if (abiturientApplicationsCount > maxApplicationsCount)
            throw new InvalidOperationException("Abiturient cannot have more than 3 applications");
        if (_specialityRepository.GetAll().All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to not existing speciality");
        if (GetApplicationsByAbiturientId(applicationDto.AbiturientId).Select(ap => ap.SpecialityId)
                                                                      .Contains(applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality you already picked");
        Application application = new()
        {
            SpecialityId = applicationDto.SpecialityId,
            AbiturientId = applicationDto.AbiturientId,
            Priority = applicationDto.Priority
        };
        return _applicationRepository.Add(application);
    }
    /// <inheritdoc />
    public void Update(int id, ApplicationCreateDto applicationDto)
    {
        var application = GetById(id);
        if (_specialityRepository.GetAll().All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to not existing speciality");
        if (GetApplicationsByAbiturientId(applicationDto.AbiturientId).Select(ap => ap.SpecialityId)
                                                                      .Contains(applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality you already picked");
        application.SpecialityId = applicationDto.SpecialityId;
        application.AbiturientId = applicationDto.AbiturientId;
        application.Priority = applicationDto.Priority;
        _applicationRepository.Update(id, application);
    }
    /// <inheritdoc />
    public void Delete(int id)
    {
        if (_applicationRepository.GetById(id) == null)
            throw new KeyNotFoundException("Cannot delete a non-existing application");
        _applicationRepository.Delete(id);
    }
    /// <inheritdoc />
    public IEnumerable<Application> GetApplicationsBySpecialityId(int specialityId)
    {
        var applications = _applicationRepository.GetAll().Where(ap => ap.SpecialityId == specialityId);

        if (applications.Count() == 0)
            throw new KeyNotFoundException("No applications found for the specified speciality");

        return applications;
    }
    /// <inheritdoc />
    public IEnumerable<Application> GetApplicationsByPriority(int priority)
    {
        var applications = _applicationRepository.GetAll().Where(ap => ap.Priority == priority);

        if (applications.Count() == 0)
            throw new KeyNotFoundException("No applications found with the specified priority");

        return applications;
    }
    /// <inheritdoc />
    public IEnumerable<Application> GetApplicationsByAbiturientId(int abiturientId)
    {
        if (_abiturientRepository.GetById(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve applications for a non-existing abiturient");

        var applications = _applicationRepository.GetAll().Where(ap => ap.AbiturientId == abiturientId);
        return applications;
    }
}