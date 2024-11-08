using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.DbContenxt
{
    public class InterviewSessionDAO
    {
        private static InterviewSessionDAO instance;
        public static InterviewSessionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InterviewSessionDAO();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<InterviewSession>> GetAllAsync()
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewSessions
                    .Include(iss => iss.InterviewRound)
                    .Include(iss => iss.SessionApplications)
                    .Include(iss => iss.SessionInterviewers)
                    .Where(iss => !iss.IsDelete)
                    .ToListAsync();
            }
        }

        public async Task<InterviewSession> GetByIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewSessions
                    .Include(iss => iss.InterviewRound)
                    .Include(iss => iss.SessionApplications)
                        .ThenInclude(sa => sa.Application)
                            .ThenInclude(app => app.User)
                    .Include(iss => iss.SessionInterviewers)
                    .FirstOrDefaultAsync(iss => iss.Id == id && !iss.IsDelete);
            }
        }

        public async Task<IEnumerable<InterviewSession>> FindAsync(Expression<Func<InterviewSession, bool>> predicate)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewSessions
                    .Include(iss => iss.InterviewRound)
                    .Include(iss => iss.SessionApplications)
                    .Include(iss => iss.SessionInterviewers)
                    .Where(predicate)
                    .ToListAsync();
            }
        }

        public async Task<InterviewSession> AddAsync(InterviewSession interviewSession)
        {
            using (var _context = new RecuitmentDbContext())
            {
                //interviewSession.InterviewDate = DateTime.UtcNow;
                _context.InterviewSessions.Add(interviewSession);
                await _context.SaveChangesAsync();
                return interviewSession;
            }
        }

        public async Task<InterviewSession> UpdateAsync(InterviewSession interviewSession)
        {
            using (var _context = new RecuitmentDbContext())
            {
                _context.Entry(interviewSession).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return interviewSession;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewSession = await _context.InterviewSessions.FindAsync(id);
                if (interviewSession == null)
                {
                    return false;
                }

                interviewSession.IsDelete = true;
                _context.Entry(interviewSession).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> AddApplicationToSessionAsync(long sessionId, long applicationId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewSession = await _context.InterviewSessions
                    .Include(iss => iss.SessionApplications)
                    .FirstOrDefaultAsync(iss => iss.Id == sessionId && !iss.IsDelete);
                if (interviewSession == null)
                {
                    throw new KeyNotFoundException("Interview session not found.");
                }

                var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == applicationId && !a.IsDelete);
                if (application == null)
                {
                    throw new KeyNotFoundException("Application not found.");
                }

                if (interviewSession.SessionApplications.Any(sa => sa.ApplicationId == applicationId))
                {
                    throw new InvalidOperationException("Application already exists in the session.");
                }

                var sessionApplication = new SessionApplication
                {
                    InterviewSessionId = sessionId,
                    ApplicationId = applicationId,
                    Status ="PROCESSING"
                };

                interviewSession.SessionApplications.Add(sessionApplication);

                _context.InterviewSessions.Update(interviewSession);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> RemoveApplicationFromSessionAsync(long sessionId, long applicationId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewSession = await _context.InterviewSessions
                    .Include(iss => iss.SessionApplications)
                    .FirstOrDefaultAsync(iss => iss.Id == sessionId && !iss.IsDelete);
                if (interviewSession == null)
                {
                    throw new KeyNotFoundException("Interview session not found.");
                }

                var sessionApplication = interviewSession.SessionApplications
                    .FirstOrDefault(sa => sa.ApplicationId == applicationId);
                if (sessionApplication == null)
                {
                    throw new InvalidOperationException("Application does not exist in the session.");
                }

                interviewSession.SessionApplications.Remove(sessionApplication);
                _context.SessionApplications.Remove(sessionApplication);

                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> AddInterviewerToSessionAsync(long sessionId, long interviewerId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewSession = await _context.InterviewSessions
                    .Include(iss => iss.SessionInterviewers)
                    .FirstOrDefaultAsync(iss => iss.Id == sessionId && !iss.IsDelete);
                if (interviewSession == null)
                {
                    throw new KeyNotFoundException("Interview session not found.");
                }

                var interviewer = await _context.Users.FirstOrDefaultAsync(i => i.Id == interviewerId && !i.IsDelete);
                if (interviewer == null)
                {
                    throw new KeyNotFoundException("Interviewer not found.");
                }

                if (interviewSession.SessionInterviewers.Any(si => si.UserId == interviewerId))
                {
                    throw new InvalidOperationException("Interviewer already exists in the session.");
                }

                var sessionInterviewer = new SessionInterviewer
                {
                    InterviewSessionId = sessionId,
                    UserId = interviewerId
                };

                interviewSession.SessionInterviewers.Add(sessionInterviewer);

                _context.InterviewSessions.Update(interviewSession);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> RemoveInterviewerFromSessionAsync(long sessionId, long interviewerId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewSession = await _context.InterviewSessions
                    .Include(iss => iss.SessionInterviewers)
                    .FirstOrDefaultAsync(iss => iss.Id == sessionId && !iss.IsDelete);
                if (interviewSession == null)
                {
                    throw new KeyNotFoundException("Interview session not found.");
                }

                var sessionInterviewer = interviewSession.SessionInterviewers
                    .FirstOrDefault(si => si.UserId == interviewerId);
                if (sessionInterviewer == null)
                {
                    throw new InvalidOperationException("Interviewer does not exist in the session.");
                }

                interviewSession.SessionInterviewers.Remove(sessionInterviewer);
                _context.SessionInterviewers.Remove(sessionInterviewer);

                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<SessionInterviewer?> GetActiveSessionByInterviewerIdAsync(long interviewerId, DateTime date)
        {
                var _context = new RecuitmentDbContext();
                return await _context.SessionInterviewers
                .Include(si => si.InterviewSession)
                .Where(si => si.UserId == interviewerId && si.InterviewSession.IsDelete == false && si.InterviewSession.InterviewDate.Date == date.Date)
                .FirstOrDefaultAsync();
        }
        public async Task<SessionApplication?> GetActiveSessionByApplicationIdAsync(long applicationId, DateTime date)
        {
            var _context = new RecuitmentDbContext();
            return await _context.SessionApplications
                .Include(sa => sa.InterviewSession)
                .Where(sa => sa.ApplicationId == applicationId && sa.InterviewSession.IsDelete == false && sa.InterviewSession.InterviewDate.Date == date.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSessionApplicationStatusAsync(long sessionId, long applicationId, string result, string status)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var sessionApplication = await _context.SessionApplications
                    .FirstOrDefaultAsync(sa => sa.InterviewSessionId == sessionId && sa.ApplicationId == applicationId);

                if (sessionApplication == null)
                {
                    throw new KeyNotFoundException("Session application not found.");
                }

                sessionApplication.Result = result;
                sessionApplication.Status = status;

                _context.Entry(sessionApplication).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<SessionInterviewer>> GetInterviewerSessionsScheduleAsync(long interviewerId)
        {
            var _context = new RecuitmentDbContext();
            return await _context.SessionInterviewers
                .Where(sa => sa.UserId == interviewerId)
                .Include(sa => sa.InterviewSession)
                .ThenInclude(session => session.InterviewRound)
                    .ThenInclude(round => round.Job)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionApplication>> GetApplicantSessionsScheduleAsync(long applicationId)
        {
            var _context = new RecuitmentDbContext(); 
            return await _context.SessionApplications
                .Where(sa => sa.ApplicationId == applicationId)
                .Include(sa => sa.InterviewSession)
                .ThenInclude(session => session.InterviewRound)
                    .ThenInclude(round => round.Job)
                .ToListAsync();
        }


    }
}
