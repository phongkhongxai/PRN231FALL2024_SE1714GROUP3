using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public string? ResetPasswordToken { get; set; } 
        public DateTime? ResetPasswordExpiry { get; set; }

        public Role Role { get; set; }
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>(); 
        public ICollection<SessionInterviewer> SessionInterviewers { get; set; } = new List<SessionInterviewer>();
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
