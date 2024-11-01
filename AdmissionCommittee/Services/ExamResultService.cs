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
    public async Task<IEnumerable<ExamResultDto>> GetAllAsync()
    {
        var results = await _examResultRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExamResultDto>>(results);
    }

    /// <inheritdoc />
    public async Task<ExamResultDto> GetByIdAsync(int id)
    {
        var result = await _examResultRepository.GetByIdAsync(id);
        if (result == null)
            throw new KeyNotFoundException("Exam result not found");

        return _mapper.Map<ExamResultDto>(result);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(ExamResultCreateDto examResultDto)
    {
        if (await _abiturientRepository.GetByIdAsync(examResultDto.AbiturientId) == null)
            throw new InvalidOperationException("Adding result to a non-existing abiturient");

        var isExamAlreadyAdded = (await _examResultRepository
                                 .GetAllAsync())
                                 .Any(er => er.AbiturientId == examResultDto.AbiturientId
                                             && er.ExamName == examResultDto.ExamName);
        if (isExamAlreadyAdded)
            throw new InvalidOperationException("Adding result to an already added exam");

        var examResult = _mapper.Map<ExamResult>(examResultDto);
        return await _examResultRepository.AddAsync(examResult);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, ExamResultCreateDto examResultDto)
    {
        var examResult = await GetByIdAsync(id);
        _mapper.Map(examResultDto, examResult);
        await _examResultRepository.UpdateAsync(_mapper.Map<ExamResult>(examResult));
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var result = await _examResultRepository.GetByIdAsync(id);
        if (result == null)
            throw new KeyNotFoundException("Cannot delete a non-existing exam result");

        await _examResultRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExamResultDto>> GetResultsByAbiturientIdAsync(int abiturientId)
    {
        if (await _abiturientRepository.GetByIdAsync(abiturientId) == null)
            throw new InvalidOperationException("Cannot retrieve results for a non-existing abiturient");

        var results = (await _examResultRepository
                      .GetAllAsync())
                      .Where(er => er.AbiturientId == abiturientId)
                      .ToList();
        if (results.Count == 0)
            throw new KeyNotFoundException("No exam results found for the abiturient");

        return _mapper.Map<IEnumerable<ExamResultDto>>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExamResultDto>> GetMaxResultsPerExamAsync()
    {
        var maxResults = (await _examResultRepository.GetAllAsync())
            .GroupBy(er => er.ExamName)
            .Select(g => g.OrderByDescending(er => er.Result).First())
            .Where(er => er != null)
            .ToList();

        if (maxResults.Count == 0)
            throw new InvalidOperationException("No exam results found for any exam");

        return _mapper.Map<IEnumerable<ExamResultDto>>(maxResults);
    }
}
