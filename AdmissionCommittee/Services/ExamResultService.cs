using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// Service for managing exam results of abiturients.
/// This service provides business logic for managing the lifecycle of exam results,
/// such as adding, updating, retrieving, and deleting results. It interacts with the
/// exam result repository to perform CRUD operations and includes filtering methods based on
/// abiturient and exam name.
/// </summary>
public class ExamResultService(
    IExamResultRepository examResultRepository,
    IAbiturientRepository abiturientRepository) : IExamResultService
{
    private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
    private readonly IExamResultRepository _examResultRepository = examResultRepository;
    /// <inheritdoc />
    public IEnumerable<ExamResult> GetAll() => _examResultRepository.GetAll();
    /// <inheritdoc />
    public ExamResult GetById(int id)
    {
        var result = _examResultRepository.GetById(id);
        if(result == null)
            throw new KeyNotFoundException("Exam result not found");
        return result;
    }
    /// <inheritdoc />
    public void Add(ExamResultDto examResultDto)
    {
        if(_abiturientRepository.GetById(examResultDto.AbiturientId) == null)
           throw new InvalidOperationException("Adding result to not existing abiturient");
        if(_examResultRepository.GetAll().Where(er => er.AbiturientId == examResultDto.AbiturientId)
                                         .Select(er => er.ExamName).Contains(examResultDto.ExamName))
            throw new InvalidOperationException("Adding result to already added exam");
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
        if (_examResultRepository.GetById(examResultDto.Id) == null)
            throw new KeyNotFoundException("Cannot update a non-existing exam result");
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
    public void Delete(int id)
    {
        if (_examResultRepository.GetById(id) == null)
            throw new KeyNotFoundException("Cannot delete a non-existing exam result");
        _examResultRepository.Delete(id);
    }

    /// <inheritdoc />
    public IEnumerable<ExamResult> GetResultsByAbiturientId(int abiturientId)
    {
        if (_abiturientRepository.GetById(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve results for a non-existing abiturient");
        var results = _examResultRepository.GetAll().Where(er => er.AbiturientId == abiturientId);
        if (!results.Any())
            throw new KeyNotFoundException("No exam results found for the abiturient");
        return results;
    }

    /// <inheritdoc />
    public IEnumerable<ExamResult> GetMaxResultsPerExam()
    {
        var maxResults = _examResultRepository.GetAll().Where(er => er != null)
               .GroupBy(er => er.ExamName)
               .Select(g => g.OrderByDescending(er => er.Result).First())
               .Where(er => er != null);
        if (!maxResults.Any())
            throw new InvalidOperationException("No exam results found for any exam");

        return maxResults;
    }
}
