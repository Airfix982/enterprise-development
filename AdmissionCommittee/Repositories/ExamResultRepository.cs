using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repository for managing ExamResult entities in the database.
/// </summary>
public class ExamResultRepository(ApplicationDbContext context) : IExamResultRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<ExamResult>> GetAllAsync()
    {
        return await _context.ExamResults.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ExamResult?> GetByIdAsync(int id)
    {
        return await _context.ExamResults.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(ExamResult entity)
    {
        await _context.ExamResults.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ExamResult entity)
    {
        _context.ExamResults.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var examResult = await _context.ExamResults.FindAsync(id);
        if (examResult != null)
        {
            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();
        }
    }
}
