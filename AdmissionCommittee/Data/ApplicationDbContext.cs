using Microsoft.EntityFrameworkCore;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Abiturient> Abiturients { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Abiturient>()
            .HasKey(ab => ab.Id);

        modelBuilder.Entity<Abiturient>()
            .Property(ab => ab.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Application>()
            .HasKey(ap => ap.Id);

        modelBuilder.Entity<Application>()
            .Property(ap => ap.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ExamResult>()
            .HasKey(er => er.Id);

        modelBuilder.Entity<ExamResult>()
            .Property(er => er.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Speciality>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Speciality>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

    }
}
