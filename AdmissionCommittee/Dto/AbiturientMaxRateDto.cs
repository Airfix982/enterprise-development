using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto for an abiturient and his favorite speciality
    /// </summary>
    public class AbiturientMaxRateDto
    {
        /// <summary>
        /// An abiturient model
        /// </summary>
        public required Abiturient Abiturient { get; set; }
        /// <summary>
        /// The speciality id
        /// </summary>
        public required int FavoriteSpecialityId { get; set; }
    }
}
