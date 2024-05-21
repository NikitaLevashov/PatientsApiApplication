using Microsoft.EntityFrameworkCore;
using PatientsDAL.Models;

namespace PatientsDAL
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientName>().HasKey(p => p.Id);
            modelBuilder
            .Entity<Patient>()
            .HasOne(u => u.Name)
            .WithOne(p => p.Patient)
            .HasForeignKey<PatientName>(p => p.PatientKey)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Patient> Patients { get; set; } = null!;
    }
}