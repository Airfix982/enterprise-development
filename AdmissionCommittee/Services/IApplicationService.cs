using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services
{
    public interface IApplicationService
    {
        /// <summary>
        /// Get ids of students who selected the speciality
        /// </summary>
        /// <param name="specialityId">A unique id of an speciality</param>
        /// <returns>A collection of Applications</returns>
        public IEnumerable<Application> GetApplicationsBySpecialityId(int specialityId);
        /// <summary>
        /// Get all aplication with the priority level
        /// </summary>
        /// <param name="priority">number of priority</param>
        /// <returns>A collection of Applications</returns>
        public IEnumerable<Application> GetApplicationsByPriority(int priority);
    }
}
