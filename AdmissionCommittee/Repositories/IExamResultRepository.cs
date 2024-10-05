using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Exam result repository interface
    /// </summary>
    public interface IExamResultRepository : IRepositoryInMemory<ExamResult>
    {
    }
}
