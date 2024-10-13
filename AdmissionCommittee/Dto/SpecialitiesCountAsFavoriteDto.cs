namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto of a count of abiturients who selected the speciality as fvorite one
    /// </summary>
    public class SpecialitiesCountAsFavoriteDto
    {
        /// <summary>
        /// Amount of abiturients
        /// </summary>
        public int AbiturientsCount { get; set; }
        /// <summary>
        /// The speciality id
        /// </summary>
        public int SpecialityId { get; set; }
    }
}
