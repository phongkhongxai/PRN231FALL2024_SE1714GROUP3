using BusinessObjects.DTO;
using System;
using System.Collections.Generic;

namespace BusinessObjects.DTOs
{
    public class InterviewSessionDTO
    {
        public long Id { get; set; }
        public long InterviewRoundId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Position { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public InterviewRoundDTO InterviewRound { get; set; } 
        public ICollection<SessionApplicationDTO> SessionApplications { get; set; } = new List<SessionApplicationDTO>();
        public ICollection<SessionInterviewerDTO> SessionInterviewers { get; set; } = new List<SessionInterviewerDTO>();
    }
}
