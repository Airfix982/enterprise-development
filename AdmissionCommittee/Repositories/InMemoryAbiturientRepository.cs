using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    public class InMemoryAbiturientRepository : RepositoryInMemory<Abiturient>, IAbiturientRepository
    {
        /// <summary>
        /// Updates an existing abiturient in the in-memory context.
        /// </summary>
        /// <param name="Abiturient">The abiturient with updated values.</param>
        public new void Update(Abiturient abiturient)
        {
            var existingAbiturient = GetById(abiturient.Id);
            if (existingAbiturient != null)
            {
                existingAbiturient.Name = abiturient.Name;
                existingAbiturient.LastName = abiturient.LastName;
                existingAbiturient.BirthdayDate = abiturient.BirthdayDate;
                existingAbiturient.Country = abiturient.Country;
                existingAbiturient.City = abiturient.City;
            }
        }
    }
}
