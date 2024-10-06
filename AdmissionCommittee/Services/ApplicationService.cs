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
    /// Service for managing applications submitted by abiturients for various specialities.
    /// This service provides business logic for managing the lifecycle of applications,
    /// such as adding, updating, retrieving, and deleting applications. It interacts with the
    /// application repository to perform CRUD operations and includes filtering methods based on
    /// speciality, priority, and abiturient.
    /// </summary>
    internal class ApplicationService(
        IApplicationRepository applicationRepository) : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository = applicationRepository;
        /// <inheritdoc />
        public IEnumerable<Application> GetAll() => _applicationRepository.GetAll();
        /// <inheritdoc />
        public Application? GetById(int id) => _applicationRepository.GetById(id);
        /// <inheritdoc />
        public void Add(ApplicationDto applicationDto)
        {
            Application application = new
            {
                Id = applicationDto.Id,
                SpecialityId = applicationDto.SpecialityId,
                AbiturientId = applicationDto.AbiturientId,
                Priority = applicationDto.Priority
            };
            _applicationRepository.Add(application);
        }
        /// <inheritdoc />
        public void Update(ApplicationDto applicationDto)
        {
            Application application = new
            {
                Id = applicationDto.Id,
                SpecialityId = applicationDto.SpecialityId,
                AbiturientId = applicationDto.AbiturientId,
                Priority = applicationDto.Priority
            };
            _applicationRepository.Update(application);
        }
        /// <inheritdoc />
        public void Delete(int id) => _applicationRepository.Delete(id);
        /// <inheritdoc />
        public IEnumerable<Application> GetApplicationsBySpecialityId(int specialityId)
        {
            return _applicationRepository.GetAll().Where(ap => ap.SpecialityId == specialityId);    
        }
        /// <inheritdoc />
        public IEnumerable<Application> GetApplicationsByPriority(int priority)
        {
            return _applicationRepository.GetAll().Where(ap => ap.Priority == priority);
        }
        /// <inheritdoc />
        public IEnumerable<Application> GetApplicationsByAbiturientId(int abiturientId)
        {
            return _applicationRepository.GetAll().Where(ap => ap.AbiturientId == abiturientId);
        }
    }
}
