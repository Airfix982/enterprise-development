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
    public IEnumerable<AbiturientDto> GetAll()
    {
        var abiturients = _abiturientRepository.GetAll();
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public AbiturientDto GetById(int id)
    {
        var abiturient = _abiturientRepository.GetById(id);
        if (abiturient == null)
            throw new KeyNotFoundException("Abiturient not found");
        return _mapper.Map<AbiturientDto>(abiturient);
    }
    /// <inheritdoc />
    public int Add(AbiturientCreateDto abiturientDto)
    {
        var abiturient = _mapper.Map<Abiturient>(abiturientDto);
        return _abiturientRepository.Add(abiturient);
    }
    /// <inheritdoc />
    public void Update(int id, AbiturientCreateDto abiturientDto)
    {
        var abiturient = _mapper.Map<Abiturient>(GetById(id));
        _mapper.Map(abiturientDto, abiturient);
        _abiturientRepository.Update(id, abiturient);
    }
    /// <inheritdoc />
    public void Delete(int id)
    {
        if (GetById(id) == null)
            throw new KeyNotFoundException("Abiturient not found");
        _abiturientRepository.Delete(id);
    }
    /// <inheritdoc />
    public IEnumerable<AbiturientDto> GetAbiturientsByCity(string city)
    {
        var abiturients = _abiturientRepository
                          .GetAll()
                          .Where(e => e.City == city);
        if (abiturients.Count() == 0)
            throw new KeyNotFoundException("Abiturients not found");
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public IEnumerable<AbiturientDto> GetAbiturientsOlderThan(int age)
    {
        var abiturients = _abiturientRepository
                          .GetAll()
                          .Where(e => DateTime.Now.Year - e.BirthdayDate.Year > age);
        if (abiturients.Count() == 0)
            throw new KeyNotFoundException("Abiturients not found");
        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }
    /// <inheritdoc />
    public IEnumerable<AbiturientDto> GetAbiturientBySpecialityOrderedByRates(int specialityId)
    {
        if (_specialityRepository.GetById(specialityId) == null)
            throw new InvalidOperationException($"No speciality with id: {specialityId}");

        var abiturients = _applicationRepository.GetAll()
            .Where(ap => ap.SpecialityId == specialityId)
            .Join(_abiturientRepository.GetAll(),
                  ap => ap.AbiturientId,
                  ab => ab.Id,
                  (ap, ab) => ab)
            .GroupJoin(_examResultRepository.GetAll(),
                       ab => ab.Id,
                       er => er.AbiturientId,
                       (abiturient, examResults) => new
                       {
                           Abiturient = abiturient,
                           TotalResult = examResults.Sum(r => r.Result)
                       })
            .OrderByDescending(x => x.TotalResult)
            .Select(x => x.Abiturient);

        if (abiturients.Count() == 0)
            throw new KeyNotFoundException("Abiturients not found");

        return _mapper.Map<IEnumerable<AbiturientDto>>(abiturients);
    }

    /// <inheritdoc />
    public IEnumerable<SpecialitiesCountAsFavoriteDto> GetAbiturientsCountByFirstPrioritySpecialities()
    {
        var firstPriority = 1;

        var applicationsWithFirstPriority = _applicationRepository.GetAll()
                                            .Where(ap => ap.Priority == firstPriority);

        var result = _specialityRepository.GetAll()
                    .GroupJoin(applicationsWithFirstPriority,
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
    public IEnumerable<AbiturientWithExamScoresDto> GetTopRatedAbiturients(int maxCount)
    {
        var abiturients = _abiturientRepository.GetAll()
            .GroupJoin(_examResultRepository.GetAll(),
                       ab => ab.Id,
                       er => er.AbiturientId,
                       (abiturient, examResults) => new
                       {
                           Abiturient = abiturient,
                           ResultsSum = examResults.Sum(r => r.Result)
                       })
            .OrderByDescending(ab => ab.ResultsSum)
            .Take(maxCount);

        if (abiturients.Count() == 0)
            throw new KeyNotFoundException("No abiturients available");

        return abiturients.Select(ab => new AbiturientWithExamScoresDto
        {
            abiturient = ab.Abiturient,
            resultsSum = ab.ResultsSum
        });
    }

    /// <inheritdoc />
    public IEnumerable<AbiturientMaxRateDto> GetMaxRatedAbiturienstWithFavoriteSpeciality()
    {
        var allExamResults = _examResultRepository.GetAll().ToList();
        var maxResultsAbiturientsIds = allExamResults
            .GroupBy(er => er.ExamName)
            .Select(g => g.OrderByDescending(er => er.Result).First())
            .Select(r => r.AbiturientId)
            .Distinct()
            .ToList();

        if (maxResultsAbiturientsIds.Count() == 0)
        {
            throw new KeyNotFoundException("No exam results found to calculate top-rated abiturients.");
        }

        var firstPriority = 1;
        var allApplications = _applicationRepository.GetAll()
            .Where(ap => ap.Priority == firstPriority)
            .ToList();

        return _abiturientRepository
              .GetAll()
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