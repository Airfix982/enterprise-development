using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// In-memory repository for managing exam results.
    /// </summary>
    public class InMemoryExamResultRepository : RepositoryInMemory<ExamResult>, IExamResultRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryExamResultRepository"/> class.
        /// </summary>
        public InMemoryExamResultRepository() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryExamResultRepository"/> class with initial data.
        /// </summary>
        /// <param name="initData">List of initial exam results.</param>
        public InMemoryExamResultRepository(List<ExamResult> initData) : base(initData) { }

        /// <summary>
        /// Updates an existing exam result with new values.
        /// </summary>
        /// <param name="examResult">The exam result with updated values.</param>
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
