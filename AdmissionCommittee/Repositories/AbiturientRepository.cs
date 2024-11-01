using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repository for managing Abiturient entities in the database.
/// </summary>
public class AbiturientRepository(ApplicationDbContext context) : IAbiturientRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<Abiturient>> GetAllAsync()
    {
        return await _context.Abiturients.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Abiturient?> GetByIdAsync(int id)
    {
        return await _context.Abiturients.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(Abiturient entity)
    {
        await _context.Abiturients.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Abiturient entity)
    {
        _context.Abiturients.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var abiturient = await _context.Abiturients.FindAsync(id);
        if (abiturient != null)
        {
            _context.Abiturients.Remove(abiturient);
            await _context.SaveChangesAsync();
        }
    }
}
