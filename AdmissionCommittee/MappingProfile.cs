using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Abiturient, AbiturientDto>().ReverseMap();
        CreateMap<Abiturient, AbiturientCreateDto>().ReverseMap();
        CreateMap<Application, ApplicationDto>().ReverseMap();
        CreateMap<Application, ApplicationCreateDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultCreateDto>().ReverseMap();
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
        CreateMap<Speciality, SpecialityCreateDto>().ReverseMap();
    }
}
