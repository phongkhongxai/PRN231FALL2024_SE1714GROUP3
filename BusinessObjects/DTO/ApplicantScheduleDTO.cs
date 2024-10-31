using BusinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ApplicantScheduleDTO
    {
        public long InterviewSessionId { get; set; }
        public string Location { get; set; }
        public DateTime InterviewDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string JobTitle { get; set; }
        public int RoundNumber { get; set; } 
        public string RoundName { get; set; }

    }
}
