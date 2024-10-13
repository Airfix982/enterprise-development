using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto for exam result.
    /// </summary>
    public class ExamResultDto : IDto
    {
        /// <summary>
        /// Unique Id of the exam result.
        /// </summary>
        [Required]
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        /// <summary>
        /// Id of the abiturient who took the exam.
        /// </summary>
        [Required]
        [JsonPropertyName("abiturient_id")]
        public required int AbiturientId { get; set; }
        /// <summary>
        /// Name of the exam.
        /// </summary>
        [Required]
        [JsonPropertyName("exam_name")]
        public required string ExamName { get; set; }
        /// <summary>
        /// Result of the abiturient in the exam.
        /// </summary>
        [Required]
        [JsonPropertyName("result")]
        [Range(0, 100, ErrorMessage = "Result must be between 0 `n` 100.")]
        public required int Result { get; set; }
    }
}