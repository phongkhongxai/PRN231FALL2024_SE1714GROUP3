using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class RecuitmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public RecuitmentDbContext(DbContextOptions<RecuitmentDbContext> options) : base(options)
        {
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
                .HasForeignKey(a => a.JobId);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany(a => a.Applications)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Interview>()
                .HasOne(i => i.Application)
                .WithMany(i => i.Interviews)
                .HasForeignKey(i => i.ApplicationId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Interview)
                .WithMany(s => s.Schedules)
                .HasForeignKey(s => s.InterviewId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.User)
                .WithMany(s => s.Schedules)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
