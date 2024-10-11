using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace AdmissionCommittee.Domain.Services
{
    /// <summary>
    /// Service for managing specialities offered by the university.
    /// This service provides business logic for managing the lifecycle of specialities,
    /// such as adding, updating, retrieving, and deleting specialities.
    /// </summary>
    public class SpecialityService(
        ISpecialityRepository specialityRepository) : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository = specialityRepository;
        /// <inheritdoc />
        public IEnumerable<Speciality> GetAll()
        {
            return _specialityRepository.GetAll();
        }
        /// <inheritdoc />
        public Speciality? GetById(int id)
        {
            return _specialityRepository.GetById(id);
        }
        /// <inheritdoc />
        public void Add(SpecialityDto specialityDto)
        {
            Speciality speciality = new()
            {
                Id = specialityDto.Id,
                Name = specialityDto.Name,
                Number = specialityDto.Number,
                Facility = specialityDto.Facility
            };
            _specialityRepository.Add(speciality);
        }
        /// <inheritdoc />
        public void Update(SpecialityDto specialityDto)
        {
            Speciality speciality = new()
            {
                Id = specialityDto.Id,
                Name = specialityDto.Name,
                Number = specialityDto.Number,
                Facility = specialityDto.Facility
            };
            _specialityRepository.Update(speciality);
        }
        /// <inheritdoc />
        public void Delete(int id)
        {
            _specialityRepository.Delete(id);
        }
    }
}
