using BusinessObjects.DTO;
using System;

namespace BusinessObjects.DTOs
{
    public class SessionInterviewerDTO
    {
        public long UserId { get; set; }
        public long InterviewSessionId { get; set; }
        public UserDTO User { get; set; }
    }
}
