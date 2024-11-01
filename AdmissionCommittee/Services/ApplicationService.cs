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
    public async Task<IEnumerable<ApplicationDto>> GetAllAsync()
    {
        var applications = await _applicationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    /// <inheritdoc />
    public async Task<ApplicationDto> GetByIdAsync(int id)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        if (application == null)
            throw new KeyNotFoundException("Application not found");
        return _mapper.Map<ApplicationDto>(application);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(ApplicationCreateDto applicationDto)
    {
        if (await _abiturientRepository.GetByIdAsync(applicationDto.AbiturientId) == null)
            throw new InvalidOperationException("Application to non-existing abiturient");

        var abiturientApplicationsCount = (await GetApplicationsByAbiturientIdAsync(applicationDto.AbiturientId)).ToList();
        var maxApplicationsCount = 3;

        if (abiturientApplicationsCount.Count >= maxApplicationsCount)
            throw new InvalidOperationException("Abiturient cannot have more than 3 applications");

        if ((await _specialityRepository.GetAllAsync()).All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to non-existing speciality");

        if (abiturientApplicationsCount.Any(ap => ap.SpecialityId == applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality already picked");

        var application = _mapper.Map<Application>(applicationDto);
        return await _applicationRepository.AddAsync(application);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, ApplicationCreateDto applicationDto)
    {
        var application = await GetByIdAsync(id);

        if ((await _specialityRepository.GetAllAsync()).All(s => s.Id != applicationDto.SpecialityId))
            throw new InvalidOperationException("Application to non-existing speciality");

        if ((await GetApplicationsByAbiturientIdAsync(applicationDto.AbiturientId))
            .Any(ap => ap.SpecialityId == applicationDto.SpecialityId))
            throw new InvalidOperationException("Double application to the speciality already picked");

        _mapper.Map(applicationDto, application);
        await _applicationRepository.UpdateAsync(_mapper.Map<Application>(application));
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        if (application == null)
            throw new KeyNotFoundException("Cannot delete a non-existing application");

        await _applicationRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApplicationDto>> GetApplicationsBySpecialityIdAsync(int specialityId)
    {
        var applications = await _applicationRepository.GetAllAsync();
        var applicationsList = applications.Where(ap => ap.SpecialityId == specialityId)
                           .ToList();

        if (applicationsList.Count == 0)
            throw new KeyNotFoundException("No applications found for the specified speciality");

        return _mapper.Map<IEnumerable<ApplicationDto>>(applicationsList);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApplicationDto>> GetApplicationsByPriorityAsync(int priority)
    {
        var applications = await _applicationRepository.GetAllAsync();
        var applicationsList = applications.Where(ap => ap.Priority == priority)
                           .ToList();

        if (applicationsList.Count == 0)
            throw new KeyNotFoundException("No applications found with the specified priority");

        return _mapper.Map<IEnumerable<ApplicationDto>>(applicationsList);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApplicationDto>> GetApplicationsByAbiturientIdAsync(int abiturientId)
    {
        if (await _abiturientRepository.GetByIdAsync(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve applications for a non-existing abiturient");

        var applications = await _applicationRepository.GetAllAsync();
        var applicationsList = applications.Where(ap => ap.AbiturientId == abiturientId).ToList();
        return _mapper.Map<IEnumerable<ApplicationDto>>(applicationsList);
    }
}
