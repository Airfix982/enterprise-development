using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories;

public class ExamResultRepository(ApplicationDbContext context) : IExamResultRepository
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<ExamResult> GetAll() => _context.ExamResults.ToList();
    public ExamResult? GetById(int id) => _context.ExamResults.Find(id);
    public int Add(ExamResult entity)
    {
        _context.ExamResults.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }
    public void Update(ExamResult entity)
    {
        _context.ExamResults.Update(entity);
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        var abiturient = _context.ExamResults.Find(id);
        if (abiturient != null)
        {
            _context.ExamResults.Remove(abiturient);
            _context.SaveChanges();
        }
    }
}