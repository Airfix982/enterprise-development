using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;

namespace AdmissionCommittee.Domain.Services
{
    /// <summary>
    /// Service that manages operations related to Abiturient entities, including filtering, 
    /// ordering by exam results, and working with related Application and ExamResult services.
    /// </summary>
    public class AbiturientService(
         IAbiturientRepository abiturientRepository,
         IApplicationService applicationRepository,
         IExamResultService examResultRepository,
         ISpecialityService specialityRepository) : IAbiturientService
    {
        private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
        private readonly IApplicationService _applicationRepository = applicationRepository;
        private readonly IExamResultService _examResultRepository = examResultRepository;
        private readonly ISpecialityService _specialityRepository = specialityRepository;
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAll() => _abiturientRepository.GetAll();
        /// <inheritdoc />
        public Abiturient GetById(int id)
        {
            var abiturient = _abiturientRepository.GetById(id);
            if (abiturient == null)
                throw new KeyNotFoundException("Abiturient not found");
            return abiturient;
        }
        /// <inheritdoc />
        public void Add(AbiturientDto abiturientDto)
        {
            Abiturient abiturient = new()
            {
                Id = abiturientDto.Id,
                Name = abiturientDto.Name,
                LastName = abiturientDto.LastName,
                BirthdayDate = abiturientDto.BirthdayDate,
                Country = abiturientDto.Country,
                City = abiturientDto.City
            };
            _abiturientRepository.Add(abiturient);
        }
        /// <inheritdoc />
        public void Update(AbiturientDto abiturientDto)
        {
            if (GetById(abiturientDto.Id) == null)
                throw new KeyNotFoundException("Abiturient not found");
            Abiturient abiturient = new()
            {
                Id = abiturientDto.Id,
                Name = abiturientDto.Name,
                LastName = abiturientDto.LastName,
                BirthdayDate = abiturientDto.BirthdayDate,
                Country = abiturientDto.Country,
                City = abiturientDto.City
            };
            _abiturientRepository.Update(abiturient);
        }
        /// <inheritdoc />
        public void Delete(int id)
        {
            if (GetById(id) == null)
                throw new KeyNotFoundException("Abiturient not found");
            _abiturientRepository.Delete(id);
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientsByCity(string city)
        {
            var abiturients = _abiturientRepository.GetAll().Where(e => e.City == city);
            if (!abiturients.Any())
                throw new KeyNotFoundException("Abiturients not found");
            return abiturients;
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientsOlderThan(int age)
        {
            var abiturients = _abiturientRepository.GetAll().Where(e => DateTime.Now.Year - e.BirthdayDate.Year > age);
            if (!abiturients.Any())
                throw new KeyNotFoundException("Abiturients not found");
            return abiturients;
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientBySpecialityOrderedByRates(int specialityId)
        {
            if (_specialityRepository.GetById(specialityId) == null)
                throw new InvalidOperationException($"No speciality with id: {specialityId}");
            List<int> abiturientsIds = _applicationRepository.GetAll().Where(ap => ap.SpecialityId == specialityId)
                                                    .Select(a => a.AbiturientId).ToList();
            var abiturients = _abiturientRepository.GetAll().Where(ab => abiturientsIds.Contains(ab.Id))
                   .OrderByDescending(a => _examResultRepository.GetAll().Where(er => er.AbiturientId == a.Id)
                                          .Sum(r => r.Result));
            if (!abiturients.Any())
                throw new KeyNotFoundException("Abiturients not found");
            return abiturients;
        }
        /// <inheritdoc />
        public IEnumerable<SpecialitiesCountAsFavoriteDto> GetAbiturientsCountByFirstPrioritySpecialities()
        {
            int firstPriority = 1;

            var allSpecialities = _specialityRepository.GetAll();
            if (!allSpecialities.Any())
            {
                throw new InvalidOperationException("No specialities available.");
            }
            var applicationsWithFirstPriority = _applicationRepository.GetAll().Where(ap => ap.Priority == firstPriority);

            var groupedApplications = applicationsWithFirstPriority
                                        .GroupBy(a => a.SpecialityId)
                                        .Select(group => new
                                        {
                                            SpecialityId = group.Key,
                                            AbiturientsCount = group.Count()
                                        })
                                        .ToDictionary(g => g.SpecialityId, g => g.AbiturientsCount);

            return allSpecialities.Select(speciality => new SpecialitiesCountAsFavoriteDto
            {
                SpecialityId = speciality.Id,
                AbiturientsCount = groupedApplications.ContainsKey(speciality.Id) ? groupedApplications[speciality.Id] : 0
            });
        }
        /// <inheritdoc />
        public IEnumerable<AbiturientWithExamScoresDto> GetTopRatedAbiturients(int maxCount)
        {
            var abiturients = _abiturientRepository.GetAll()
                .Select(ab => new
                {
                    Abiturient = ab,
                    ResultsSum = _examResultRepository.GetAll()
                        .Where(er => er.AbiturientId == ab.Id)
                        .Sum(r => r.Result)
                })
                .OrderByDescending(ab => ab.ResultsSum)
                .Take(maxCount);

            if (!abiturients.Any())
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
            var maxResultsAbiturientsIds = _examResultRepository.GetAll().Where(er => er != null)
               .GroupBy(er => er.ExamName)
               .Select(g => g.OrderByDescending(er => er.Result).First())
               .Where(er => er != null).Select(r => r.AbiturientId).Distinct().ToList();
            if (!maxResultsAbiturientsIds.Any())
            {
                throw new KeyNotFoundException("No exam results found to calculate top-rated abiturients.");
            }
            int firstPriority = 1;
            return _abiturientRepository
                  .GetAll().Where(ab => maxResultsAbiturientsIds.Contains(ab.Id))
                  .Select(ab =>
                  {
                      var application = _applicationRepository.GetAll().Where(ap => ap.AbiturientId == ab.Id)
                                                                    .Where(ap => ap.Priority == firstPriority)
                                                                    .First();
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
}