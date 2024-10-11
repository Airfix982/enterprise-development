using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    public class InMemoryExamResultRepository : RepositoryInMemory<ExamResult>, IExamResultRepository
    {
        public InMemoryExamResultRepository() : base() { }
        public InMemoryExamResultRepository(List<ExamResult> initData) : base(initData) { }
        /// <inheritdoc />
        public new void Update(ExamResult examResult)
        {
            var existingExamResult = GetById(examResult.Id);
            if (existingExamResult != null)
            {
                existingExamResult.AbiturientId = examResult.AbiturientId;
                existingExamResult.ExamName = examResult.ExamName;
                existingExamResult.Result = examResult.Result;
            }
        }
    }
}
