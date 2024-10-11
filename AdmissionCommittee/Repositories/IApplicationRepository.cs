using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Application repository interface
    /// </summary>
    public interface IApplicationRepository : IRepositoryInMemory<Application>
    {
    }
}
