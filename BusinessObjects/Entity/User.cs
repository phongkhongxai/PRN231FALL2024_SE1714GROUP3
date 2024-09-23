using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsDelete { get; set; } = false;
        public string? Gender { get; set; }
        public long RoleId { get; set; }

        public Role Role { get; set; }
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>();

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
