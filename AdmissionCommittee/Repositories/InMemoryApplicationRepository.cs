using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    public class InMemoryApplicationRepository : RepositoryInMemory<Application>, IApplicationRepository
    {
        public InMemoryApplicationRepository(List<Application> initData) : base(initData) { }
        /// <inheritdoc />
        public new void Update(Application application)
        {
            var existingApplication = GetById(application.Id);
            if (existingApplication != null)
            {
                existingApplication.SpecialityId = application.SpecialityId;
                existingApplication.AbiturientId = application.AbiturientId;
                existingApplication.Priority = application.Priority;
            }
        }
    }
}
