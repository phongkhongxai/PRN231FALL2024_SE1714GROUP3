using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entity
{
    public class InterviewRound
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long JobId { get; set; }
        public int RoundNumber { get; set; }
        public string RoundName { get; set; } 
        public string Status { get; set; }
        public string Description { get; set; } 
        public bool IsDelete { get; set; } = false; 
        public Job Job { get; set; }
        public ICollection<InterviewSession> InterviewSessions { get; set; } = new List<InterviewSession>(); 

    }
}
