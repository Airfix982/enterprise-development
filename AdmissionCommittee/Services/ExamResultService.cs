using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AutoMapper;

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
    IAbiturientRepository abiturientRepository,
    IMapper mapper) : IExamResultService
{
    private readonly IAbiturientRepository _abiturientRepository = abiturientRepository;
    private readonly IExamResultRepository _examResultRepository = examResultRepository;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc />
    public IEnumerable<ExamResultDto> GetAll()
    {
        var results = _examResultRepository.GetAll();
        return _mapper.Map<IEnumerable<ExamResultDto>>(results);
    }

    /// <inheritdoc />
    public ExamResultDto GetById(int id)
    {
        var result = _examResultRepository.GetById(id);
        if (result == null)
            throw new KeyNotFoundException("Exam result not found");

        return _mapper.Map<ExamResultDto>(result);
    }

    /// <inheritdoc />
    public int Add(ExamResultCreateDto examResultDto)
    {
        if (_abiturientRepository.GetById(examResultDto.AbiturientId) == null)
            throw new InvalidOperationException("Adding result to a non-existing abiturient");

        var isExamAlreadyAdded = _examResultRepository
                                 .GetAll()
                                 .Any(er => er.AbiturientId == examResultDto.AbiturientId 
                                             && er.ExamName == examResultDto.ExamName);
        if (isExamAlreadyAdded)
            throw new InvalidOperationException("Adding result to an already added exam");

        var examResult = _mapper.Map<ExamResult>(examResultDto);
        return _examResultRepository.Add(examResult);
    }

    /// <inheritdoc />
    public void Update(int id, ExamResultCreateDto examResultDto)
    {
        var examResult = GetById(id);
        _mapper.Map(examResultDto, examResult);
        _examResultRepository.Update(_mapper.Map<ExamResult>(examResult));
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        var result = _examResultRepository.GetById(id);
        if (result == null)
            throw new KeyNotFoundException("Cannot delete a non-existing exam result");

        _examResultRepository.Delete(id);
    }

    /// <inheritdoc />
    public IEnumerable<ExamResultDto> GetResultsByAbiturientId(int abiturientId)
    {
        if (_abiturientRepository.GetById(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve results for a non-existing abiturient");

        var results = _examResultRepository
                      .GetAll()
                      .Where(er => er.AbiturientId == abiturientId)
                      .ToList();
        if (results.Count == 0)
            throw new KeyNotFoundException("No exam results found for the abiturient");

        return _mapper.Map<IEnumerable<ExamResultDto>>(results);
    }

    /// <inheritdoc />
    public IEnumerable<ExamResultDto> GetMaxResultsPerExam()
    {
        var maxResults = _examResultRepository.GetAll()
            .GroupBy(er => er.ExamName)
            .Select(g => g.OrderByDescending(er => er.Result).First())
            .Where(er => er != null)
            .ToList();

        if (maxResults.Count == 0)
            throw new InvalidOperationException("No exam results found for any exam");

        return _mapper.Map<IEnumerable<ExamResultDto>>(maxResults);
    }
}
