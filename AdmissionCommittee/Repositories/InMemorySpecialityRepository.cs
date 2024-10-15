using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// In-memory repository for managing specialities.
/// </summary>
public class InMemorySpecialityRepository : RepositoryInMemory<Speciality>, ISpecialityRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InMemorySpecialityRepository"/> class with no initial data.
    /// </summary>
    public InMemorySpecialityRepository() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemorySpecialityRepository"/> class with initial data.
    /// </summary>
    /// <param name="initData">Initial list of specialities.</param>
    public InMemorySpecialityRepository(List<Speciality> initData) : base(initData) { }

    /// <summary>
    /// Updates an existing speciality with new data.
    /// </summary>
    /// <param name="id">The updated speciality's.</param>
    /// <param name="speciality">The updated speciality entity.</param>
    public override void Update(int id, Speciality speciality)
    {
        var existingSpeciality = GetById(id);
        if (existingSpeciality != null)
        {
            existingSpeciality.Number = speciality.Number;
            existingSpeciality.Name = speciality.Name;
            existingSpeciality.Facility = speciality.Facility;
        }
    }
}
