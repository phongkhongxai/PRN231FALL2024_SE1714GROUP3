using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entity
{
    public class InterviewSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long InterviewRoundId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Position { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }  
        public bool IsDelete { get; set; } = false; 
        public InterviewRound InterviewRound { get; set; } 
        public ICollection<SessionApplication> SessionApplications { get; set; } = new List<SessionApplication>();
        public ICollection<SessionInterviewer> SessionInterviewers { get; set; } = new List<SessionInterviewer>();

    }
}
