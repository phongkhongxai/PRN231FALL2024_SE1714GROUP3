using BusinessObjects.DTO;
using System;

namespace BusinessObjects.DTOs
{
    public class SessionApplicationDTO
    {
        public long ApplicationId { get; set; }
        public long InterviewSessionId { get; set; }
        public string? Result { get; set; }
        public string Status { get; set; }
        public ApplicationDTO Application { get; set; }
    }
}
