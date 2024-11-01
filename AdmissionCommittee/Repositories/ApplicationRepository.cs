using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repository for managing Application entities in the database.
/// </summary>
public class ApplicationRepository(ApplicationDbContext context) : IApplicationRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<Application>> GetAllAsync()
    {
        return await _context.Applications.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Application?> GetByIdAsync(int id)
    {
        return await _context.Applications.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(Application entity)
    {
        await _context.Applications.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Application entity)
    {
        _context.Applications.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}
