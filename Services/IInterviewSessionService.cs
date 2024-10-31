using BusinessObjects.DTO;
using BusinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInterviewSessionService
    {
        Task<IEnumerable<InterviewSessionDTO>> GetAllSessionsAsync();
        Task<InterviewSessionDTO> GetSessionByIdAsync(long id);
        Task<InterviewSessionDTO> CreateSessionAsync(InterviewSessionCreateDTO interviewSessionCreateDTO);
        Task<InterviewSessionDTO> UpdateSessionAsync(long id, InterviewSessionUpdateDTO interviewSessionUpdateDTO);
        Task<bool> DeleteSessionAsync(long id);
        Task<bool> UpdateSessionApplicationStatusAsync(long sessionId, long applicationId, string result, string status);
        Task<IEnumerable<InterviewerScheduleDTO>> GetScheduleForInterviewerAsync(long interviewerId);
        Task<IEnumerable<ApplicantScheduleDTO>> GetScheduleForApplicantAsync(long applicationId);
    }
}
