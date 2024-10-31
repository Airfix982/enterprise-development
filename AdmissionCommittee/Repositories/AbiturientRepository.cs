using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories;

public class AbiturientRepository(ApplicationDbContext context) : IAbiturientRepository
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<Abiturient> GetAll() => _context.Abiturients.ToList();
    public Abiturient? GetById(int id) => _context.Abiturients.Find(id);
    public int Add(Abiturient entity)
    {
        _context.Abiturients.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }
    public void Update(Abiturient entity)
    {
        _context.Abiturients.Update(entity);
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        var abiturient = _context.Abiturients.Find(id);
        if(abiturient != null)
        {
            _context.Abiturients.Remove(abiturient);
            _context.SaveChanges();
        }
    }
}
