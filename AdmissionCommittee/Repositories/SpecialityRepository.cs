using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Repository for managing Speciality entities in the database.
/// </summary>
public class SpecialityRepository(ApplicationDbContext context) : ISpecialityRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<Speciality>> GetAllAsync()
    {
        return await _context.Specialities.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Speciality?> GetByIdAsync(int id)
    {
        return await _context.Specialities.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(Speciality entity)
    {
        await _context.Specialities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Speciality entity)
    {
        _context.Specialities.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var speciality = await _context.Specialities.FindAsync(id);
        if (speciality != null)
        {
            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();
        }
    }
}
