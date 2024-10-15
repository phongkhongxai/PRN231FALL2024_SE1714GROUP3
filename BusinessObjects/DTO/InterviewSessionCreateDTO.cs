using System;

namespace BusinessObjects.DTOs
{
    public class InterviewSessionCreateDTO
    {
        public long InterviewRoundId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Position { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public List<long> ApplicationIds { get; set; } = new List<long>();
        public List<long> InterviewerIds { get; set; } = new List<long>();
    }
}
