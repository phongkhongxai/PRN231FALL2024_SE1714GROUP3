using System;

namespace BusinessObjects.DTOs
{
    public class InterviewSessionUpdateDTO
    { 
        public long? InterviewRoundId { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? Position { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Status { get; set; }
        public List<long> ApplicationIdsToAdd { get; set; } = new List<long>();
        public List<long> ApplicationIdsToRemove { get; set; } = new List<long>();
        public List<long> InterviewerIdsToAdd { get; set; } = new List<long>();
        public List<long> InterviewerIdsToRemove { get; set; } = new List<long>();
    }
}
