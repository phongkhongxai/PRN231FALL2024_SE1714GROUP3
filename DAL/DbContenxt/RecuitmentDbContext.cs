using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessObjects.Entity;

namespace DAL.DbContenxt
{
    public class RecuitmentDbContext : DbContext
    {
        public RecuitmentDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Resume> Resumes { get; set; } 
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<InterviewSession> InterviewSessions { get; set; }
        public DbSet<InterviewRound> InterviewRounds { get; set; }
        public DbSet<SessionApplication> SessionApplications { get; set; }
        public DbSet<SessionInterviewer> SessionInterviewers { get; set; }



        public RecuitmentDbContext(DbContextOptions<RecuitmentDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());

            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DB"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSkill>()
                .HasKey(js => new { js.JobId, js.SkillId });

            modelBuilder.Entity<JobSkill>()
                .HasOne(js => js.Job)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(j => j.JobId);

            modelBuilder.Entity<JobSkill>()
                .HasOne(j => j.Skill)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(j => j.SkillId);

            modelBuilder.Entity<UserSkill>()
                .HasKey(u => new { u.UserId, u.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(u => u.Skill)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(u => u.SkillId);

            modelBuilder.Entity<SessionApplication>()
                .HasKey(js => new { js.ApplicationId, js.InterviewSessionId });

            modelBuilder.Entity<SessionApplication>()
                .HasOne(js => js.Application)
                .WithMany(j => j.SessionApplications)
                .HasForeignKey(j => j.ApplicationId);

            modelBuilder.Entity<SessionApplication>()
                .HasOne(j => j.InterviewSession)
                .WithMany(j => j.SessionApplications)
                .HasForeignKey(j => j.InterviewSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SessionInterviewer>()
                .HasKey(js => new { js.UserId, js.InterviewSessionId });

            modelBuilder.Entity<SessionInterviewer>()
                .HasOne(js => js.User)
                .WithMany(j => j.SessionInterviewers)
                .HasForeignKey(j => j.UserId);

            modelBuilder.Entity<SessionInterviewer>()
                .HasOne(j => j.InterviewSession)
                .WithMany(j => j.SessionInterviewers)
                .HasForeignKey(j => j.InterviewSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(a => a.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany(a => a.Applications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewSession>()
                .HasOne(i => i.InterviewRound)
                .WithMany(i => i.InterviewSessions)
                .HasForeignKey(i => i.InterviewRoundId);

            modelBuilder.Entity<InterviewRound>()
                .HasOne(s => s.Job)
                .WithMany(s => s.InterviewRounds)
                .HasForeignKey(s => s.JobId); 

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Resume>()
                .HasOne(r => r.User) 
                .WithMany(r => r.Resumes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Resume)
                .WithMany(a => a.Applications)
                .HasForeignKey(a => a.ResumeId)
                .OnDelete(DeleteBehavior.Restrict); ;  

        }
    }
}
