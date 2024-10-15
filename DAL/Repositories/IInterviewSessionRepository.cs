using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IInterviewSessionRepository
    {
        Task<IEnumerable<InterviewSession>> GetAllInterviewSessionsAsync();
        Task<InterviewSession> GetInterviewSessionByIdAsync(long id);
        Task<InterviewSession> CreateInterviewSessionAsync(InterviewSession interviewSession);
        Task<InterviewSession> UpdateInterviewSessionAsync(InterviewSession interviewSession);
        Task<bool> DeleteInterviewSessionAsync(long id);
        Task<bool> AddApplicationToSessionAsync(long sessionId, long applicationId);
        Task<bool> RemoveApplicationFromSessionAsync(long sessionId, long applicationId);
        Task<bool> AddInterviewerToSessionAsync(long sessionId, long interviewerId);
        Task<bool> RemoveInterviewerFromSessionAsync(long sessionId, long interviewerId);
        Task<SessionInterviewer?> GetActiveSessionByInterviewerIdAsync(long interviewerId, DateTime date);
        Task<SessionApplication?> GetActiveSessionByApplicationIdAsync(long applicationId, DateTime date);
    }
}

