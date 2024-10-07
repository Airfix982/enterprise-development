using System;
using System.Text.Json.Serialization;

namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto for an abiturient.
    /// </summary>
    public class AbiturientDto : IDto
    {
        /// <summary>
        /// Unique Id of the abiturient.
        /// </summary>
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        /// <summary>
        /// First name of the abiturient.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        /// <summary>
        /// Last name of the abiturient.
        /// </summary>
        [JsonPropertyName("last_name")]
        public required string LastName { get; set; }
        /// <summary>
        /// Date of birth of the abiturient.
        /// </summary>
        [JsonPropertyName("birthday_date")]
        public required DateTime BirthdayDate { get; set; }
        /// <summary>
        /// Country of the abiturient.
        /// </summary>
        [JsonPropertyName("country")]
        public required string Country { get; set; }
        /// <summary>
        /// City of the abiturient.
        /// </summary>
        [JsonPropertyName("city")]
        public required string City { get; set; }
    }
}
