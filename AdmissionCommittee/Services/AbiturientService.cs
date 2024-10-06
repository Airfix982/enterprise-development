using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services
{
    /// <summary>
    /// Service that manages operations related to Abiturient entities, including filtering, 
    /// ordering by exam results, and working with related Application and ExamResult services.
    /// </summary>
    public class AbiturientService(
         IAbiturientRepository abiturientRepository,
         IApplicationService applicationService,
         IExamResultService examResultService) : IAbiturientService
    {
        private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
        private readonly IApplicationService _applicationService = applicationService;
        private readonly IExamResultService _examResultService = examResultService;
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAll() => _abiturientRepository.GetAll();
        /// <inheritdoc />
        public Abiturient? GetById(int id) => _abiturientRepository.GetById(id);
        /// <inheritdoc />
        public void Add(AbiturientDto abiturientDto)
        {
            Abiturient abiturient = new
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
            Abiturient abiturient = new
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
        public void Delete(int id) => _abiturientRepository.Delete(id);
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientsByCity(string city)
        {
            return _abiturientRepository.GetAll().Where(e => e.City == city);
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientsOlderThan(int age)
        {
            return _abiturientRepository.GetAll().Where(e => DateTime.Now.Year - e.BirthdayDate.Year > age);
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetAbiturientBySpecialityOrderedByRates(int specialityId)
        {
            List<int> abiturientsIds = _applicationService.GetApplicationsBySpecialityId(specialityId)
                                                    .Select(a => a.AbiturientId).ToList();
            return _abiturientRepository.GetAll().Where(ab => abiturientsIds.Contains(ab.Id))
                   .OrderByDescending(a => _examResultService
                                          .GetResultsByAbiturientId(a.Id).Sum(r => r.Result));
        }
        /// <inheritdoc />
        public IEnumerable<SpecialitiesCountAsFavoriteDto> GetAbiturientsCountByFirstPrioritySpecialities()
        {
            int firstPriority = 1;
            var applicationsWithFirstPriority = _applicationService.GetApplicationsByPriority(firstPriority);
            return applicationsWithFirstPriority
                   .GroupBy(a => a.SpecialityId)
                   .Select(group => new SpecialitiesCountAsFavoriteDto
                   {
                       SpecialityId = group.Key,
                       AbiturientsCount = group.Count()
                   });
        }
        /// <inheritdoc />
        public IEnumerable<Abiturient> GetTopRatedAbiturients(int maxCount)
        {
            return _abiturientRepository.GetAll().OrderByDescending(a => _examResultService
                                                                         .GetResultsByAbiturientId(a.Id)
                                                                         .Sum(r => r.Result)).Take(maxCount);
        }
        /// <inheritdoc />
        public IEnumerable<AbiturientMaxRateDto> GetMaxRatedAbiturienstWithFavoriteSpeciality()
        {
            var maxResultsAbiturientsIds = _examResultService.GetMaxResultsPerExam().Select(r => r.AbiturientId)
                                                             .Distinct().ToList();
            int firstPriority = 1;
            return _abiturientRepository
                  .GetAll().Where(ab => maxResultsAbiturientsIds.Contains(ab.Id))
                  .Select(ab => new AbiturientMaxRateDto
                  {
                      Abiturient = ab,
                      FavoriteSpecialityId = _applicationService.GetApplicationsByAbiturientId(ab.Id)
                                                                .Where(ap => ap.Priority == firstPriority)
                                                                .Select(ap => ap.SpecialityId).First()
                  });
        }
    }
}
