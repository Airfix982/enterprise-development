using AdmissionCommittee.Domain;
using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Tests.Fixtures;

public class AdmissionComitteeFixture : IDisposable
{
    public IAbiturientService AbiturientService { get; private set; }
    public IApplicationService ApplicationService { get; private set; }
    public IExamResultService ExamResultService { get; private set; }
    public ISpecialityService SpecialityService { get; private set; }

    private readonly ApplicationDbContext _context;
    public IMapper Mapper { get; private set; }
    public AdmissionComitteeFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper = config.CreateMapper();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);

        SeedData();

        AbiturientService = new AbiturientService(new AbiturientRepository(_context),
                                                  new ApplicationRepository(_context),
                                                  new ExamResultRepository(_context),
                                                  new SpecialityRepository(_context),
                                                  Mapper);
        ApplicationService = new ApplicationService(new AbiturientRepository(_context),
                                                    new SpecialityRepository(_context),
                                                    new ApplicationRepository(_context),
                                                    Mapper);
        ExamResultService = new ExamResultService(new ExamResultRepository(_context),
                                                  new AbiturientRepository(_context),
                                                  Mapper);
        SpecialityService = new SpecialityService(new SpecialityRepository(_context), Mapper);
    }

    private void SeedData()
    {
        _context.Abiturients.AddRange(
            new Abiturient { BirthdayDate = new DateTime(2005, 5, 18), City = "Moscow", Country = "Russia", Name = "Ivan", LastName = "Ivanov" },
            new Abiturient { BirthdayDate = new DateTime(2003, 6, 11), City = "Samara", Country = "Russia", Name = "Pasha", LastName = "Pavlov" },
            new Abiturient { BirthdayDate = new DateTime(2004, 11, 8), City = "Rostov", Country = "Russia", Name = "Roma", LastName = "Romanov" },
            new Abiturient { BirthdayDate = new DateTime(2003, 1, 13), City = "Saint Petersburg", Country = "Russia", Name = "Anna", LastName = "Batrakova" },
            new Abiturient { BirthdayDate = new DateTime(2004, 5, 22), City = "Samara", Country = "Russia", Name = "Dmitry", LastName = "Dmitrov" },
            new Abiturient { BirthdayDate = new DateTime(2003, 2, 5), City = "Novosibirsk", Country = "Russia", Name = "Olga", LastName = "Sidorova" },
            new Abiturient { BirthdayDate = new DateTime(2005, 1, 28), City = "Yekaterinburg", Country = "Russia", Name = "Sergey", LastName = "Sergeev" },
            new Abiturient { BirthdayDate = new DateTime(2005, 8, 1), City = "Nizhny Novgorod", Country = "Russia", Name = "Natalia", LastName = "Ivanova" },
            new Abiturient { BirthdayDate = new DateTime(2003, 9, 7), City = "Chelyabinsk", Country = "Russia", Name = "Mikhail", LastName = "Sidorov" },
            new Abiturient { BirthdayDate = new DateTime(2004, 11, 16), City = "Omsk", Country = "Russia", Name = "Ivan", LastName = "Kamarov" }
        );
        _context.Applications.AddRange(
            new Application { AbiturientId = 0, SpecialityId = 0, Priority = 1 },
            new Application { AbiturientId = 0, SpecialityId = 1, Priority = 2 },
            new Application { AbiturientId = 0, SpecialityId = 2, Priority = 3 },
            new Application { AbiturientId = 1, SpecialityId = 3, Priority = 1 },
            new Application { AbiturientId = 1, SpecialityId = 4, Priority = 2 },
            new Application { AbiturientId = 2, SpecialityId = 5, Priority = 1 },
            new Application { AbiturientId = 3, SpecialityId = 0, Priority = 1 },
            new Application { AbiturientId = 3, SpecialityId = 6, Priority = 2 },
            new Application { AbiturientId = 3, SpecialityId = 7, Priority = 3 },
            new Application { AbiturientId = 4, SpecialityId = 8, Priority = 1 },
            new Application { AbiturientId = 4, SpecialityId = 9, Priority = 2 },
            new Application { AbiturientId = 5, SpecialityId = 1, Priority = 1 },
            new Application { AbiturientId = 5, SpecialityId = 3, Priority = 2 },
            new Application { AbiturientId = 6, SpecialityId = 2, Priority = 1 },
            new Application { AbiturientId = 7, SpecialityId = 4, Priority = 1 },
            new Application { AbiturientId = 7, SpecialityId = 5, Priority = 2 },
            new Application { AbiturientId = 7, SpecialityId = 6, Priority = 3 },
            new Application { AbiturientId = 8, SpecialityId = 7, Priority = 1 },
            new Application { AbiturientId = 8, SpecialityId = 8, Priority = 2 },
            new Application { AbiturientId = 9, SpecialityId = 9, Priority = 1 }
        );
        _context.ExamResults.AddRange(
            new ExamResult { AbiturientId = 0, ExamName = "Mathematics", Result = 82 },
            new ExamResult { AbiturientId = 0, ExamName = "Physics", Result = 75 },
            new ExamResult { AbiturientId = 0, ExamName = "Computer Science", Result = 90 },
            new ExamResult { AbiturientId = 1, ExamName = "Mathematics", Result = 88 },
            new ExamResult { AbiturientId = 1, ExamName = "Chemistry", Result = 79 },
            new ExamResult { AbiturientId = 1, ExamName = "English", Result = 85 },
            new ExamResult { AbiturientId = 2, ExamName = "Biology", Result = 70 },
            new ExamResult { AbiturientId = 2, ExamName = "Physics", Result = 68 },
            new ExamResult { AbiturientId = 2, ExamName = "History", Result = 72 },
            new ExamResult { AbiturientId = 3, ExamName = "Mathematics", Result = 95 },
            new ExamResult { AbiturientId = 3, ExamName = "English", Result = 89 },
            new ExamResult { AbiturientId = 3, ExamName = "Computer Science", Result = 92 },
            new ExamResult { AbiturientId = 4, ExamName = "History", Result = 78 },
            new ExamResult { AbiturientId = 4, ExamName = "Geography", Result = 82 },
            new ExamResult { AbiturientId = 4, ExamName = "Literature", Result = 75 },
            new ExamResult { AbiturientId = 5, ExamName = "Physics", Result = 80 },
            new ExamResult { AbiturientId = 5, ExamName = "Mathematics", Result = 85 },
            new ExamResult { AbiturientId = 5, ExamName = "Chemistry", Result = 88 },
            new ExamResult { AbiturientId = 6, ExamName = "English", Result = 65 },
            new ExamResult { AbiturientId = 6, ExamName = "Mathematics", Result = 70 },
            new ExamResult { AbiturientId = 6, ExamName = "Physics", Result = 75 },
            new ExamResult { AbiturientId = 7, ExamName = "Biology", Result = 90 },
            new ExamResult { AbiturientId = 7, ExamName = "Chemistry", Result = 85 },
            new ExamResult { AbiturientId = 7, ExamName = "Physics", Result = 88 },
            new ExamResult { AbiturientId = 8, ExamName = "Mathematics", Result = 60 },
            new ExamResult { AbiturientId = 8, ExamName = "Computer Science", Result = 65 },
            new ExamResult { AbiturientId = 8, ExamName = "English", Result = 78 },
            new ExamResult { AbiturientId = 9, ExamName = "History", Result = 92 },
            new ExamResult { AbiturientId = 9, ExamName = "Geography", Result = 88 },
            new ExamResult { AbiturientId = 9, ExamName = "Physics", Result = 85 }
        );
        _context.Specialities.AddRange(
            new Speciality { Number = "105003D", Name = "Cyber Security", Facility = "Informatics" },
            new Speciality { Number = "205004A", Name = "Mechanical Engineering", Facility = "Engineering" },
            new Speciality { Number = "305005B", Name = "Business Administration", Facility = "Business" },
            new Speciality { Number = "405006C", Name = "Psychology", Facility = "Social Sciences" },
            new Speciality { Number = "505007D", Name = "Biology", Facility = "Natural Sciences" },
            new Speciality { Number = "605008E", Name = "Architecture", Facility = "Arts and Architecture" },
            new Speciality { Number = "705009F", Name = "Law", Facility = "Law School" },
            new Speciality { Number = "805010G", Name = "Medicine", Facility = "Medical School" },
            new Speciality { Number = "905011H", Name = "Environmental Studies", Facility = "Environmental Sciences" },
            new Speciality { Number = "1005012I", Name = "Philosophy", Facility = "Humanities" }
        );
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
