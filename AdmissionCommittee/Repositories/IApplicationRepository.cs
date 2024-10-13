using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Application repository interface
    /// </summary>
    public interface IApplicationRepository : IRepositoryInMemory<Application>
    {
    }
}
