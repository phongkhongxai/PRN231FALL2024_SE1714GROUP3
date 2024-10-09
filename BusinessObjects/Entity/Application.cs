using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entity
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
        public long UserId { get; set; }
        public string? Status { get; set; }
        public long ResumeId { get; set; }
        public bool IsDelete { get; set; } = false; 
        public Resume Resume { get; set; } 
        public Job Job { get; set; }
        public User User { get; set; }
        public  ICollection<SessionApplication> SessionApplications { get; set; } = new List<SessionApplication>();
    }
}
