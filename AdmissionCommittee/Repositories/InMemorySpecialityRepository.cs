using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    public class InMemorySpecialityRepository : RepositoryInMemory<Speciality>, ISpecialityRepository
    {
        /// <inheritdoc />
        public new void Update(Speciality speciality)
        {
            var existingSpeciality = GetById(speciality.Id);
            if (existingSpeciality != null)
            {
                existingSpeciality.Number = speciality.Number;
                existingSpeciality.Name = speciality.Name;
                existingSpeciality.Facility = speciality.Facility;
            }
        }
    }
}
