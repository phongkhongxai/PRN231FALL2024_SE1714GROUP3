using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Entity
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
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public RecuitmentDbContext(DbContextOptions<RecuitmentDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-MKM1I2A\\PHONGTT;uid=sa;pwd=12345;database=RecuitmentDB;TrustServerCertificate=True;");

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

            modelBuilder.Entity<Interview>()
                .HasOne(i => i.Application)
                .WithMany(i => i.Interviews)
                .HasForeignKey(i => i.ApplicationId);

            modelBuilder.Entity<Interview>()
                .HasOne(s => s.Schedule)
                .WithMany(s => s.Interviews)
                .HasForeignKey(s => s.ScheduleId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.User)
                .WithMany(s => s.Schedules)
                .HasForeignKey(s => s.UserId);

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
