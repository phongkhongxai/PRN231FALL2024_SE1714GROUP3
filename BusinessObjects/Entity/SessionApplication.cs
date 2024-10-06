using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entity
{
    public class SessionApplication
    {
        public long ApplicationId { get; set; }
        public long InterviewSessionId { get; set; }
        public string? Result { get; set; }
        public string Status { get; set; }  
        public Application Application { get; set; }
        public InterviewSession InterviewSession { get; set; }
    }
}
