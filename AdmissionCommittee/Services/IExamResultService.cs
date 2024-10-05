using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services
{
    internal class IExamResultService
    {
        /// <summary>
        /// Get all the exam results by an abiturient id
        /// </summary>
        /// <param name="id">An abiturient id</param>
        /// <returns>A collection of exam results</returns>
        public IEnumerable<ExamResult> GetResultsByAbiturientId(int id);
    }
}
