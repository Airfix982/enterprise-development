using System;
using System.Text.Json.Serialization;

namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto for an application submitted by an abiturient.
    /// </summary>
    public class ApplicationDto : IDto
    {
        /// <summary>
        /// Unique Id of the application.
        /// </summary>
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        /// <summary>
        /// Id of the speciality applied for.
        /// </summary>
        [JsonPropertyName("speciality_id")]
        public required int SpecialityId { get; set; }
        /// <summary>
        /// Id of the abiturient who submitted the application.
        /// </summary>
        [JsonPropertyName("abiturient_id")]
        public required int AbiturientId { get; set; }
        /// <summary>
        /// Priority of the application. Lower values indicate higher priority.
        /// </summary>
        [JsonPropertyName("priority")]
        public required int Priority { get; set; }
    }
}
