using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entity
{
    public class SessionInterviewer
    {
        public long UserId { get; set; }
        public long InterviewSessionId { get; set; } 
        public User User { get; set; }
        public InterviewSession InterviewSession { get; set; }
    }
}
