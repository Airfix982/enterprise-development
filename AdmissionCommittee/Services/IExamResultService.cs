using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for exam results service
/// </summary>
public interface IExamResultService : IBaseService<ExamResult, ExamResultDto, ExamResultCreateDto>
{
    /// <summary>
    /// Get all the exam results by an abiturient id
    /// </summary>
    /// <param name="id">An abiturient id</param>
    /// <returns>A collection of exam results</returns>
    public IEnumerable<ExamResult> GetResultsByAbiturientId(int id);
    /// <summary>
    /// Get max results per each exam
    /// </summary>
    /// <returns>A collection of exam results</returns>
    public IEnumerable<ExamResult> GetMaxResultsPerExam();
}