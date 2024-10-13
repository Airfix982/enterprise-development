using AdmissionCommittee.Domain.Models;

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
