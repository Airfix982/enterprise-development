using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Speciality repository interface
    /// </summary>
    public interface ISpecialityRepository : IRepositoryInMemory<Speciality>
    {
    }
}
