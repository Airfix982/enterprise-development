using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AutoMapper;

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
    IApplicationRepository applicationRepository,
    IMapper mapper) : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository = applicationRepository;
    private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc />
    public IEnumerable<ApplicationDto> GetAll()
    {
        var applications = _applicationRepository.GetAll();
        return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    /// <inheritdoc />
    public ApplicationDto GetById(int id)
    {
        var application = _applicationRepository.GetById(id);
        if (application == null)
            throw new KeyNotFoundException("Application not found");
        return _mapper.Map<ApplicationDto>(application);
    }

    /// <inheritdoc />
    public int Add(ApplicationCreateDto applicationDto)
    {
        if (_abiturientRepository.GetById(applicationDto.AbiturientId) == null)
            throw new InvalidOperationException("Application to non-existing abiturient");

        var abiturientApplicationsCount = GetApplicationsByAbiturientId(applicationDto.AbiturientId).ToList();
        var maxApplicationsCount = 3;

        if (abiturientApplicationsCount.Count >= maxApplicationsCount)
            throw new InvalidOperationException("Abiturient cannot have more than 3 applications");

        if (_specialityRepository.GetAll().All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to non-existing speciality");

        if (abiturientApplicationsCount.Any(ap => ap.SpecialityId == applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality already picked");

        var application = _mapper.Map<Application>(applicationDto);
        return _applicationRepository.Add(application);
    }

    /// <inheritdoc />
    public void Update(int id, ApplicationCreateDto applicationDto)
    {
        var application = GetById(id);

        if (_specialityRepository.GetAll().All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to non-existing speciality");

        if (GetApplicationsByAbiturientId(applicationDto.AbiturientId)
            .Any(ap => ap.SpecialityId == applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality already picked");

        _mapper.Map(applicationDto, application);
        _applicationRepository.Update(id, _mapper.Map<Application>(application));
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        var application = _applicationRepository.GetById(id);
        if (application == null)
            throw new KeyNotFoundException("Cannot delete a non-existing application");

        _applicationRepository.Delete(id);
    }

    /// <inheritdoc />
    public IEnumerable<ApplicationDto> GetApplicationsBySpecialityId(int specialityId)
    {
        var applications = _applicationRepository
                           .GetAll()
                           .Where(ap => ap.SpecialityId == specialityId)
                           .ToList();

        if (applications.Count == 0)
            throw new KeyNotFoundException("No applications found for the specified speciality");

        return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    /// <inheritdoc />
    public IEnumerable<ApplicationDto> GetApplicationsByPriority(int priority)
    {
        var applications = _applicationRepository
                           .GetAll()
                           .Where(ap => ap.Priority == priority)
                           .ToList();

        if (applications.Count == 0)
            throw new KeyNotFoundException("No applications found with the specified priority");

        return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    /// <inheritdoc />
    public IEnumerable<ApplicationDto> GetApplicationsByAbiturientId(int abiturientId)
    {
        if (_abiturientRepository.GetById(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve applications for a non-existing abiturient");

        var applications = _applicationRepository
                           .GetAll()
                           .Where(ap => ap.AbiturientId == abiturientId)
                           .ToList();
        return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }
}
