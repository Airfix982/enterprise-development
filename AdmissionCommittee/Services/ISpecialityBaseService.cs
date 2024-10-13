using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for speciality service
/// </summary>
public interface ISpecialityService : IBaseService<Speciality, SpecialityDto>
{
}