using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories;

public class SpecialityRepository(ApplicationDbContext context) : ISpecialityRepository
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<Speciality> GetAll() => _context.Specialities.ToList();
    public Speciality? GetById(int id) => _context.Specialities.Find(id);
    public int Add(Speciality entity)
    {
        _context.Specialities.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }
    public void Update(Speciality entity)
    {
        _context.Specialities.Update(entity);
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        var abiturient = _context.Specialities.Find(id);
        if (abiturient != null)
        {
            _context.Specialities.Remove(abiturient);
            _context.SaveChanges();
        }
    }
}
