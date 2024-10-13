using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Dto;
/// <summary>
/// Dto for an abiturient and sum of his results
/// </summary>
public class AbiturientWithExamScoresDto
{
    /// <summary>
    /// An abiturient
    /// </summary>
    public required Abiturient abiturient { get; set; }
    /// <summary>
    /// Sum of abiturients result scores
    /// </summary>
    public required int resultsSum { get; set; }
}
