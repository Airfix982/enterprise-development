using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories;

public class ApplicationRepository(ApplicationDbContext context) : IApplicationRepository
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<Application> GetAll() => _context.Applications.ToList();
    public Application? GetById(int id) => _context.Applications.Find(id);
    public int Add(Application entity)
    {
        _context.Applications.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }
    public void Update(Application entity)
    {
        _context.Applications.Update(entity);
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        var application = _context.Applications.Find(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            _context.SaveChanges();
        }
    }
}
