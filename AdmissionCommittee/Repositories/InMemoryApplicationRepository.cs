using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// In-memory implementation of the application repository.
/// </summary>
public class InMemoryApplicationRepository : RepositoryInMemory<Application>, IApplicationRepository
{
    /// <summary>
    /// Default constructor for in-memory application repository.
    /// </summary>
    public InMemoryApplicationRepository() : base() { }

    /// <summary>
    /// Constructor that initializes the repository with a list of applications.
    /// </summary>
    /// <param name="applicationList">List of applications to initialize the repository with.</param>
    public InMemoryApplicationRepository(List<Application> applicationList) : base(applicationList) { }
    /// <inheritdoc />
    public override Task UpdateAsync(Application application)
    {
        var existingApplication = GetByIdAsync(application.Id).Result;
        if (existingApplication != null)
        {
            existingApplication.SpecialityId = application.SpecialityId;
            existingApplication.AbiturientId = application.AbiturientId;
            existingApplication.Priority = application.Priority;
        }
        return Task.CompletedTask;
    }
}
