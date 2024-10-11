using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Dto
{
    /// <summary>
    /// Dto for speciality
    /// </summary>
    public class SpecialityDto : IDto
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        /// <summary>
        /// Name of speciality
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        /// <summary>
        /// Number of speciality
        /// </summary>
        [JsonPropertyName("number")]
        public required string Number { get; set; }
        /// <summary>
        /// Facility of speciality
        /// </summary>
        [JsonPropertyName("facility")]
        public required string Facility { get; set; }
    }
}
