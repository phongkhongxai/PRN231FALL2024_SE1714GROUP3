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
        private readonly IMapper _mapper;

        public InterviewSessionService(IInterviewSessionRepository interviewSessionRepository, IMapper mapper, IApplicationRepository applicationRepository, IUserRepository userRepository)
        {
            _interviewSessionRepository = interviewSessionRepository;
            _mapper = mapper;
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
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
            }

            // Tạo session sau khi tất cả các kiểm tra đã thành công
            var session = _mapper.Map<InterviewSession>(interviewSessionCreateDTO);
            var createdSession = await _interviewSessionRepository.CreateInterviewSessionAsync(session);
            if (createdSession == null)
            {
                throw new Exception("Session creation failed.");
            }

            // Thêm các interviewer vào session
            foreach (var interviewerId in interviewSessionCreateDTO.InterviewerIds)
            {
                await _interviewSessionRepository.AddInterviewerToSessionAsync(createdSession.Id, interviewerId);
            }

            // Thêm các application vào session
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
                }
            }
             
            if (interviewSessionUpdateDTO.ApplicationIdsToRemove != null)
            {
                foreach (var applicationId in interviewSessionUpdateDTO.ApplicationIdsToRemove)
                {
                    await _interviewSessionRepository.RemoveApplicationFromSessionAsync(session.Id, applicationId);
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
             
            var updatedSession = await _interviewSessionRepository.GetInterviewSessionByIdAsync(id); // Lấy lại session để đảm bảo tất cả thay đổi đã được áp dụng
            return _mapper.Map<InterviewSessionDTO>(updatedSession);
        }

    }
}
