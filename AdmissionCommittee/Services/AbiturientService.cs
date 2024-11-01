using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AutoMapper;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// Service that manages operations related to Abiturient entities, including filtering, 
/// ordering by exam results, and working with related Application and ExamResult services.
/// </summary>
public class AbiturientService(
     IAbiturientRepository abiturientRepository,
     IApplicationRepository applicationRepository,
     IExamResultRepository examResultRepository,
     ISpecialityRepository specialityRepository,
     IMapper mapper) : IAbiturientService
{
    private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
    private readonly IApplicationRepository _applicationRepository = applicationRepository;
    private readonly IExamResultRepository _examResultRepository = examResultRepository;
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    private readonly IMapper _mapper = mapper;
    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientDto>> GetAllAsync()
    {
        var abiturients = await _abiturientRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public async Task<AbiturientDto> GetByIdAsync(int id)
    {
        var abiturient = await _abiturientRepository.GetByIdAsync(id);
        if (abiturient == null)
            throw new KeyNotFoundException("Abiturient not found");
        return _mapper.Map<AbiturientDto>(abiturient);
    }
    /// <inheritdoc />
    public async Task<int> AddAsync(AbiturientCreateDto abiturientDto)
    {
        var abiturient = _mapper.Map<Abiturient>(abiturientDto);
        return await _abiturientRepository.AddAsync(abiturient);
    }
    /// <inheritdoc />
    public async Task UpdateAsync(int id, AbiturientCreateDto abiturientDto)
    {
        var abiturient = await _abiturientRepository.GetByIdAsync(id);
        if (abiturient == null)
            throw new KeyNotFoundException("Abiturient not found");
        _mapper.Map(abiturientDto, abiturient);
        await _abiturientRepository.UpdateAsync(abiturient);
    }
    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var abiturient = await _abiturientRepository.GetByIdAsync(id);
        if (abiturient == null)
            throw new KeyNotFoundException("Abiturient not found");
        await _abiturientRepository.DeleteAsync(id);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientDto>> GetAbiturientsByCityAsync(string city)
    {
        var abiturients = (await _abiturientRepository.GetAllAsync()).Where(e => e.City == city).ToList();
        if (abiturients.Count == 0)
            throw new KeyNotFoundException("Abiturients not found");
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientDto>> GetAbiturientsOlderThanAsync(int age)
    {
        var abiturients = (await _abiturientRepository.GetAllAsync()).Where(e => DateTime.Now.Year - e.BirthdayDate.Year > age)
                            .ToList();
        if (abiturients.Count == 0)
            throw new KeyNotFoundException("Abiturients not found");
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientDto>> GetAbiturientBySpecialityOrderedByRatesAsync(int specialityId)
    {
        if (await _specialityRepository.GetByIdAsync(specialityId) == null)
            throw new InvalidOperationException($"No speciality with id: {specialityId}");

        var abiturients = (await _applicationRepository.GetAllAsync())
            .Where(ap => ap.SpecialityId == specialityId)
            .Join(await _abiturientRepository.GetAllAsync(),
                  ap => ap.AbiturientId,
                  ab => ab.Id,
                  (ap, ab) => ab)
            .GroupJoin(await _examResultRepository.GetAllAsync(),
                       ab => ab.Id,
                       er => er.AbiturientId,
                       (abiturient, examResults) => new
                       {
                           Abiturient = abiturient,
                           TotalResult = examResults.Sum(r => r.Result)
                       })
            .OrderByDescending(x => x.TotalResult)
            .Select(x => x.Abiturient)
            .ToList();

        if (abiturients.Count == 0)
            throw new KeyNotFoundException("Abiturients not found");

        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SpecialitiesCountAsFavoriteDto>> GetAbiturientsCountByFirstPrioritySpecialitiesAsync()
    {
        var firstPriority = 1;

        var applicationsWithFirstPriority = (await _applicationRepository.GetAllAsync()).Where(ap => ap.Priority == firstPriority);

        var result = (await _specialityRepository.GetAllAsync()).GroupJoin(applicationsWithFirstPriority,
                               speciality => speciality.Id,
                               application => application.SpecialityId,
                               (speciality, applications) => new SpecialitiesCountAsFavoriteDto
                               {
                                   SpecialityId = speciality.Id,
                                   AbiturientsCount = applications.Count()
                               })
                    .ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException("No specialities available.");
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientWithExamScoresDto>> GetTopRatedAbiturientsAsync(int maxCount)
    {
        var abiturients = (await _abiturientRepository.GetAllAsync()).GroupJoin(await _examResultRepository.GetAllAsync(),
                       ab => ab.Id,
                       er => er.AbiturientId,
                       (abiturient, examResults) => new
                       {
                           Abiturient = abiturient,
                           ResultsSum = examResults.Sum(r => r.Result)
                       })
            .OrderByDescending(ab => ab.ResultsSum)
            .Take(maxCount)
            .ToList();

        if (abiturients.Count == 0)
            throw new KeyNotFoundException("No abiturients available");

        return abiturients.Select(ab => new AbiturientWithExamScoresDto
        {
            Abiturient = ab.Abiturient,
            ResultsSum = ab.ResultsSum
        });
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AbiturientMaxRateDto>> GetMaxRatedAbiturientsWithFavoriteSpecialityAsync()
    {
        var allExamResults = (await _examResultRepository.GetAllAsync()).ToList();
        var maxResultsAbiturientsIds = allExamResults
            .GroupBy(er => er.ExamName)
            .Select(g => g.OrderByDescending(er => er.Result).First())
            .Select(r => r.AbiturientId)
            .Distinct()
            .ToList();

        if (maxResultsAbiturientsIds.Count == 0)
        {
            throw new KeyNotFoundException("No exam results found to calculate top-rated abiturients.");
        }

        var firstPriority = 1;
        var allApplications = (await _applicationRepository.GetAllAsync())
            .Where(ap => ap.Priority == firstPriority)
            .ToList();

        return (await _abiturientRepository.GetAllAsync())
              .Where(ab => maxResultsAbiturientsIds.Contains(ab.Id))
              .Select(ab =>
              {
                  var application = allApplications
                      .FirstOrDefault(ap => ap.AbiturientId == ab.Id);

                  if (application == null)
                  {
                      throw new InvalidOperationException($"No first-priority application found for abiturient with ID {ab.Id}.");
                  }

                  return new AbiturientMaxRateDto
                  {
                      Abiturient = ab,
                      FavoriteSpecialityId = application.SpecialityId
                  };
              });
    }

}