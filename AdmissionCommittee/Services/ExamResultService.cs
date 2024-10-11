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
    /// Service for managing exam results of abiturients.
    /// This service provides business logic for managing the lifecycle of exam results,
    /// such as adding, updating, retrieving, and deleting results. It interacts with the
    /// exam result repository to perform CRUD operations and includes filtering methods based on
    /// abiturient and exam name.
    /// </summary>
    public class ExamResultService(
        IExamResultRepository examResultRepository) : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository = examResultRepository;
        /// <inheritdoc />
        public IEnumerable<ExamResult> GetAll() => _examResultRepository.GetAll();
        /// <inheritdoc />
        public ExamResult? GetById(int id) => _examResultRepository.GetById(id);
        /// <inheritdoc />
        public void Add(ExamResultDto examResultDto)
        {
            ExamResult examResult = new()
            {
                Id = examResultDto.Id,
                AbiturientId = examResultDto.AbiturientId,
                ExamName = examResultDto.ExamName,
                Result = examResultDto.Result
            };
            _examResultRepository.Add(examResult);
        }
        /// <inheritdoc />
        public void Update(ExamResultDto examResultDto)
        {
            ExamResult examResult = new()
            {
                Id = examResultDto.Id,
                AbiturientId = examResultDto.AbiturientId,
                ExamName = examResultDto.ExamName,
                Result = examResultDto.Result
            };
            _examResultRepository.Update(examResult);
        }

        /// <inheritdoc />
        public void Delete(int id) => _examResultRepository.Delete(id);

        /// <inheritdoc />
        public IEnumerable<ExamResult> GetResultsByAbiturientId(int abiturientId)
        {
            return _examResultRepository.GetAll().Where(er => er.AbiturientId == abiturientId);
        }

        /// <inheritdoc />
        public IEnumerable<ExamResult> GetMaxResultsPerExam()
        {
            return _examResultRepository.GetAll().Where(er => er != null)
                   .GroupBy(er => er.ExamName)
                   .Select(g => g.OrderByDescending(er => er.Result).First())
                   .Where(er => er != null);
        }
    }
}
