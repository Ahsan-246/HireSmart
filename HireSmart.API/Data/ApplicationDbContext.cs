using HireSmart.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HireSmart.API.Data
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<AIEvaluation> AIEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.Recruiter)
                .WithMany()
                .HasForeignKey(j => j.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AIEvaluation>()
                .Property(a => a.MatchScore)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Application>()
               .HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Resume)
                .WithMany()
                .HasForeignKey(a => a.ResumeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany()
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}