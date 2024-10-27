using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class InterviewSessionRepository : IInterviewSessionRepository
    {
        public async Task<IEnumerable<InterviewSession>> GetAllInterviewSessionsAsync()
        {
            return await InterviewSessionDAO.Instance.GetAllAsync();
        }

        public async Task<InterviewSession> GetInterviewSessionByIdAsync(long id)
        {
            return await InterviewSessionDAO.Instance.GetByIdAsync(id);
        }

        public async Task<InterviewSession> CreateInterviewSessionAsync(InterviewSession interviewSession)
        {
            return await InterviewSessionDAO.Instance.AddAsync(interviewSession);
        }

        public async Task<InterviewSession> UpdateInterviewSessionAsync(InterviewSession interviewSession)
        {
            return await InterviewSessionDAO.Instance.UpdateAsync(interviewSession);
        }

        public async Task<bool> DeleteInterviewSessionAsync(long id)
        {
            return await InterviewSessionDAO.Instance.DeleteAsync(id);
        }

        public async Task<bool> AddApplicationToSessionAsync(long sessionId, long applicationId)
        {
            return await InterviewSessionDAO.Instance.AddApplicationToSessionAsync(sessionId, applicationId);
        }

        public async Task<bool> RemoveApplicationFromSessionAsync(long sessionId, long applicationId)
        {
            return await InterviewSessionDAO.Instance.RemoveApplicationFromSessionAsync(sessionId, applicationId);
        }

        public async Task<bool> AddInterviewerToSessionAsync(long sessionId, long interviewerId)
        {
            return await InterviewSessionDAO.Instance.AddInterviewerToSessionAsync(sessionId, interviewerId);
        }

        public async Task<bool> RemoveInterviewerFromSessionAsync(long sessionId, long interviewerId)
        {
            return await InterviewSessionDAO.Instance.RemoveInterviewerFromSessionAsync(sessionId, interviewerId);
        }

        public async Task<SessionInterviewer?> GetActiveSessionByInterviewerIdAsync(long interviewerId, DateTime date)
        {
            return await InterviewSessionDAO.Instance.GetActiveSessionByInterviewerIdAsync(interviewerId, date);
        }

        public async Task<SessionApplication?> GetActiveSessionByApplicationIdAsync(long applicationId, DateTime date)
        {
            return await InterviewSessionDAO.Instance.GetActiveSessionByApplicationIdAsync(applicationId, date);

        }

        public async Task<bool> UpdateSessionApplicationStatusAsync(long sessionId, long applicationId, string result, string status)
        {
            return await InterviewSessionDAO.Instance.UpdateSessionApplicationStatusAsync(sessionId, applicationId, result, status);

        }
    }
}
