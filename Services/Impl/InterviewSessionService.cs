using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTOs;
using BusinessObjects.Entity;
using DAL.Repositories;
using DAL.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class InterviewSessionService : IInterviewSessionService
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository; 
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public InterviewSessionService(IInterviewSessionRepository interviewSessionRepository, IMapper mapper, IApplicationRepository applicationRepository, IUserRepository userRepository, IApplicationService applicationService)
        {
            _interviewSessionRepository = interviewSessionRepository;
            _mapper = mapper;
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
            _applicationService = applicationService;
        }
        public async Task<InterviewSessionDTO> CreateSessionAsync(InterviewSessionCreateDTO interviewSessionCreateDTO)
        {
            // Kiểm tra từng interviewer
            foreach (var interviewerId in interviewSessionCreateDTO.InterviewerIds)
            {
                if (await _userRepository.GetUserByIdAsync(interviewerId) == null)
                {
                    throw new Exception($"Interviewer with ID {interviewerId} does not exist.");
                }

                var existingSession = await _interviewSessionRepository.GetActiveSessionByInterviewerIdAsync(interviewerId, interviewSessionCreateDTO.InterviewDate);
                if (existingSession != null)
                {
                    throw new Exception($"Interviewer with ID {interviewerId} is already assigned to another active session.");
                }
            }

            // Kiểm tra từng application
            foreach (var applicationId in interviewSessionCreateDTO.ApplicationIds)
            {
                // Kiểm tra xem application có tồn tại không
                if (await _applicationRepository.GetApplicationByIdAsync(applicationId) == null)
                {
                    throw new Exception($"Application with ID {applicationId} does not exist.");
                }

                var existingSession = await _interviewSessionRepository.GetActiveSessionByApplicationIdAsync(applicationId, interviewSessionCreateDTO.InterviewDate);
                if (existingSession != null)
                {
                    throw new Exception($"Application with ID {applicationId} is already part of another active session.");
                }
                var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
                if (application == null)
                {
                    throw new Exception($"Application with ID {applicationId} is already part of another active session.");
                }
                application.Status = "INTERVIEWING"; 
                await _applicationRepository.UpdateApplicationAsync(application);
            }
             
            var session = _mapper.Map<InterviewSession>(interviewSessionCreateDTO);
            var createdSession = await _interviewSessionRepository.CreateInterviewSessionAsync(session);
            if (createdSession == null)
            {
                throw new Exception("Session creation failed.");
            }
             
            foreach (var interviewerId in interviewSessionCreateDTO.InterviewerIds)
            {
                await _interviewSessionRepository.AddInterviewerToSessionAsync(createdSession.Id, interviewerId);
            }
             
            foreach (var applicationId in interviewSessionCreateDTO.ApplicationIds)
            {
                await _interviewSessionRepository.AddApplicationToSessionAsync(createdSession.Id, applicationId);
            }

            return _mapper.Map<InterviewSessionDTO>(createdSession);
        }



        public async Task<bool> DeleteSessionAsync(long id)
        {
            var session = await _interviewSessionRepository.GetInterviewSessionByIdAsync(id);
            if (session == null)
            {
                return false;
            }
            foreach (var sessionApplication in session.SessionApplications)
            {
                await _interviewSessionRepository.RemoveApplicationFromSessionAsync(sessionApplication.InterviewSessionId, sessionApplication.ApplicationId);
                var application = await _applicationRepository.GetApplicationByIdAsync(sessionApplication.ApplicationId);
                if (application == null)
                {
                    throw new Exception($"Application with ID {sessionApplication.ApplicationId} is already part of another active session.");
                }
                application.Status = "PROCESSING";
                await _applicationRepository.UpdateApplicationAsync(application);
            }
            foreach (var sessionInterviewers in session.SessionInterviewers)
            {
                await _interviewSessionRepository.RemoveInterviewerFromSessionAsync(sessionInterviewers.InterviewSessionId, sessionInterviewers.UserId); 
            }
            return await _interviewSessionRepository.DeleteInterviewSessionAsync(id);
        }

        public async Task<IEnumerable<InterviewSessionDTO>> GetAllSessionsAsync()
        {
            var sessions = await _interviewSessionRepository.GetAllInterviewSessionsAsync();
            return _mapper.Map<IEnumerable<InterviewSessionDTO>>(sessions);
        }

        public async Task<InterviewSessionDTO> GetSessionByIdAsync(long id)
        {
            var job = await _interviewSessionRepository.GetInterviewSessionByIdAsync(id);
            return _mapper.Map<InterviewSessionDTO>(job);
        }

        public async Task<bool> UpdateSessionApplicationStatusAsync(long sessionId, long applicationId, string result, string status)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                throw new ArgumentException("Result cannot be empty or whitespace.", nameof(result));
            }

            if (string.IsNullOrWhiteSpace(status) || (status != "FAIL" && status != "PASS"))
            {
                throw new ArgumentException("Status must be either 'FAIL' or 'PASS'.", nameof(status));
            } 
            bool sessionUpdateResult = await _interviewSessionRepository.UpdateSessionApplicationStatusAsync(sessionId, applicationId, result, status);
            if (!sessionUpdateResult)
            {
                return false;  
            }
             
            if (status == "FAIL")
            {
                var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
                if (application == null)
                {
                    return false;  
                }

                application.Status = "REJECTED";
                await _applicationRepository.UpdateApplicationAsync(application);
            }
            if(status == "PASS")
            {
                var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
                if (application == null)
                {
                    return false;
                }

                application.Status = "PROCESSING";
                await _applicationRepository.UpdateApplicationAsync(application);
            }

            return true;

        }

        public async Task<InterviewSessionDTO> UpdateSessionAsync(long id, InterviewSessionUpdateDTO interviewSessionUpdateDTO)
        { 
            var session = await _interviewSessionRepository.GetInterviewSessionByIdAsync(id);
            if (session == null)
            {
                return null;  
            }
             
            if (!string.IsNullOrEmpty(interviewSessionUpdateDTO.Location))
            {
                session.Location = interviewSessionUpdateDTO.Location;
            }

            if (!string.IsNullOrEmpty(interviewSessionUpdateDTO.Description))
            {
                session.Description = interviewSessionUpdateDTO.Description;
            }

            if (interviewSessionUpdateDTO.InterviewDate != DateTime.MinValue)
            {
                session.InterviewDate = interviewSessionUpdateDTO.InterviewDate;
            }

            if (!string.IsNullOrEmpty(interviewSessionUpdateDTO.Position))
            {
                session.Position = interviewSessionUpdateDTO.Position;
            }

            if (interviewSessionUpdateDTO.Duration.HasValue)
            {
                session.Duration = interviewSessionUpdateDTO.Duration.Value;
            } 
            if (!string.IsNullOrEmpty(interviewSessionUpdateDTO.Status))
            {
                session.Status = interviewSessionUpdateDTO.Status;
            }
             
            await _interviewSessionRepository.UpdateInterviewSessionAsync(session);
             
            if (interviewSessionUpdateDTO.ApplicationIdsToAdd != null)
            {
                foreach (var applicationId in interviewSessionUpdateDTO.ApplicationIdsToAdd)
                { 
                    if (await _applicationRepository.GetApplicationByIdAsync(applicationId) == null)
                    {
                        throw new Exception($"Application with ID {applicationId} does not exist.");
                    } 
                    await _interviewSessionRepository.AddApplicationToSessionAsync(session.Id, applicationId);
                    var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
                    if (application == null)
                    {
                        throw new Exception($"Application with ID {applicationId} is already part of another active session.");
                    }
                    application.Status = "INTERVIEWING";
                    await _applicationRepository.UpdateApplicationAsync(application);
                }
            }
             
            if (interviewSessionUpdateDTO.ApplicationIdsToRemove != null)
            {
                foreach (var applicationId in interviewSessionUpdateDTO.ApplicationIdsToRemove)
                {
                    await _interviewSessionRepository.RemoveApplicationFromSessionAsync(session.Id, applicationId);
                    var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
                    if (application == null)
                    {
                        throw new Exception($"Application with ID {applicationId} is already part of another active session.");
                    }
                    application.Status = "PROCESSING";
                    await _applicationRepository.UpdateApplicationAsync(application);
                }
            } 


            if (interviewSessionUpdateDTO.InterviewerIdsToAdd != null)
            {
                foreach (var interviewerId in interviewSessionUpdateDTO.InterviewerIdsToAdd)
                { 
                    if (await _userRepository.GetUserByIdAsync(interviewerId)==null)
                    {
                        throw new Exception($"Interviewer with ID {interviewerId} does not exist.");
                    } 
                    await _interviewSessionRepository.AddInterviewerToSessionAsync(session.Id, interviewerId);
                }
            }
             
            if (interviewSessionUpdateDTO.InterviewerIdsToRemove != null)
            {
                foreach (var interviewerId in interviewSessionUpdateDTO.InterviewerIdsToRemove)
                {
                    await _interviewSessionRepository.RemoveInterviewerFromSessionAsync(session.Id, interviewerId);
                }
            }
             
            var updatedSession = await _interviewSessionRepository.GetInterviewSessionByIdAsync(id);  
            return _mapper.Map<InterviewSessionDTO>(updatedSession);
        }
        public async Task<IEnumerable<InterviewerScheduleDTO>> GetScheduleForInterviewerAsync(long interviewerId)
        { 
            var sessionInterviewers = await _interviewSessionRepository.GetInterviewerSessionsScheduleAsync(interviewerId);
             
            var interviewerSchedules = sessionInterviewers
                .Select(si => new InterviewerScheduleDTO
                {
                    InterviewSessionId = si.InterviewSession.Id,
                    Location = si.InterviewSession.Location,
                    InterviewDate = si.InterviewSession.InterviewDate,
                    Duration = si.InterviewSession.Duration, 
                    JobTitle = si.InterviewSession.InterviewRound.Job.Title,
                    RoundNumber = si.InterviewSession.InterviewRound.RoundNumber,
                    RoundName = si.InterviewSession.InterviewRound.RoundName

                });

            return interviewerSchedules;
        }

        public async Task<IEnumerable<ApplicantScheduleDTO>> GetScheduleForApplicantAsync(long applicationId)
        { 
            var sessionApplications = await _interviewSessionRepository.GetApplicantSessionsScheduleAsync(applicationId);
             
            var applicantSchedules = sessionApplications
                .Select(sa => new ApplicantScheduleDTO
                {
                    InterviewSessionId = sa.InterviewSession.Id, 
                    Location = sa.InterviewSession.Location,
                    InterviewDate = sa.InterviewSession.InterviewDate,
                    Duration = sa.InterviewSession.Duration,
                    Result = sa.Result ?? "N/A",        
                    Status = sa.Status ?? "N/A",
                    JobTitle = sa.InterviewSession.InterviewRound.Job.Title,
                    RoundNumber = sa.InterviewSession.InterviewRound.RoundNumber,
                    RoundName = sa.InterviewSession.InterviewRound.RoundName
                });

            return applicantSchedules;
        }



    }
}
