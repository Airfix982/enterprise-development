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
    public class AbiturientService(
         IApplicationService applicationService,
         ISpecialityService specialityService,
         IExamResultService examResultService) : 
    {
        private IApplicationService _applicationService = applicationService;
        private ISpecialityService _specialityService = specialityService;
        private IExamResultService _examResultService = examResultService;

        public IEnumerable<Abiturient> GetAbiturientsByCity(string city)
        {
            return _context.Where(e => e.City == city);
        }
        public IEnumerable<Abiturient> GetAbiturientsOlderThan(int age)
        {
            return _context.Where(e => DateTime.Now.Year - e.BirthdayDate.Year > age);
        }
        public IEnumerable<Abiturient> GetAbiturientBySpecialityOrderedByRates(int specialityId)
        {
            var abiturientsIds = _applicationService.GetApplicationsBySpecialityId(specialityId)
                                                       .Select(a => a.AbiturientId);
            List<Abiturient> abiturients = new();
            foreach (var abiturientId in abiturientsIds)
            {
                var abiturient = GetById(abiturientId);
                if (abiturient != null)
                    abiturients.Add(abiturient);
            }
            return abiturients.
                   OrderByDescending(a => _examResultService
                                          .GetResultsByAbiturientId(a.Id).Sum(r => r.Result));
        }
        public IEnumerable<SpecialitiesCountAsFavoriteDto> GetAbiturientsCountByFirstPrioritySpecialities()
        {
            int firstPriority = 1;
            var applicationsWithFirstPriority = _applicationService.GetApplicationsByPriority(firstPriority);
            var abiturientsCountBySpecialities = applicationsWithFirstPriority
                .GroupBy(a => a.SpecialityId)
                .Select(group => new SpecialitiesCountAsFavoriteDto
                {
                    SpecialityId = group.Key,
                    AbiturientsCount = group.Count()
                });

            return abiturientsCountBySpecialities;
        }
        public IEnumerable<Abiturient> GetTopRatedAbiturients(int maxCount)
        {
            return _context.OrderByDescending(a => _examResultService
                                                   .GetResultsByAbiturientId(a.Id)
                                                   .Sum(r => r.Result)).Take(maxCount);
        }
    }
}
