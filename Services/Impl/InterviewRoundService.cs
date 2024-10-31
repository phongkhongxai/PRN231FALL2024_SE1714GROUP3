using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class InterviewRoundService: IInterviewRoundService
    {
        private readonly IInterviewRoundRepository _interviewRoundRepository;
        private readonly IMapper _mapper;

        public InterviewRoundService(IInterviewRoundRepository interviewRoundRepository, IMapper mapper)
        {
            _interviewRoundRepository = interviewRoundRepository;
            _mapper = mapper;
        }

        public async Task<InterviewRoundDTO> CreateInterviewRoundAsync(InterviewRoundCreateDTO interviewRoundCreateDto)
        {
            var interviewRound = _mapper.Map<InterviewRound>(interviewRoundCreateDto);
            interviewRound.Status = "PREPARE";
            var createdInterviewRound = await _interviewRoundRepository.AddAsync(interviewRound);
            return _mapper.Map<InterviewRoundDTO>(createdInterviewRound);
        }

        public async Task<bool> DeleteInterviewRoundAsync(long id)
        {
            var interviewRound = await _interviewRoundRepository.GetByIdAsync(id);
            if (interviewRound == null)
            {
                return false;
            }
            return await _interviewRoundRepository.DeleteAsync(id);
        }
         

        public async Task<InterviewRoundDTO> GetInterviewRoundByIdAsync(long id)
        {
            var interviewRound = await _interviewRoundRepository.GetByIdAsync(id);
            return _mapper.Map<InterviewRoundDTO>(interviewRound);
        }
        public async Task<IEnumerable<InterviewRoundDTO>> GetAllInterviewRoundsAsync()
        {
            var interviewRounds = await _interviewRoundRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InterviewRoundDTO>>(interviewRounds);
        }
        public async Task<IEnumerable<InterviewRoundDTO>> GetInterviewRoundsByJobIdAsync(long jobId)
        {
            var interviewRounds = await _interviewRoundRepository.GetInterviewRoundsByJobIdAsync(jobId);
            return _mapper.Map<IEnumerable<InterviewRoundDTO>>(interviewRounds);
        }

        public async Task<InterviewRoundDTO> UpdateInterviewRoundAsync(long id, InterviewRoundUpdateDTO interviewRoundUpdateDto)
        {
            var interviewRound = await _interviewRoundRepository.GetByIdAsync(id);
            if (interviewRound == null)
            {
                return null;
            }
             
            if (interviewRoundUpdateDto.RoundName != null)
            {
                interviewRound.RoundName = interviewRoundUpdateDto.RoundName;
            }

            if (interviewRoundUpdateDto.Description != null)
            {
                interviewRound.Description = interviewRoundUpdateDto.Description;
            }

            if (interviewRoundUpdateDto.RoundNumber.HasValue)
            {
                interviewRound.RoundNumber = interviewRoundUpdateDto.RoundNumber.Value;
            }

            var updatedInterviewRound = await _interviewRoundRepository.UpdateAsync(interviewRound);
            return _mapper.Map<InterviewRoundDTO>(updatedInterviewRound);
        }

        public async Task<InterviewRoundDTO> UpdateStatusInterviewRoundAsync(long id, string status)
        {
            var interviewRound = await _interviewRoundRepository.GetByIdAsync(id);
            if (interviewRound == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(status) || (status != "DONE" && status != "PROCESSING" && status != "PREPARE"))
            {
                throw new ArgumentException("Status must be either 'DONE' or 'PROCESSING' or 'PREPARE'.", nameof(status));
            }
            interviewRound.Status = status; 

            var updatedInterviewRound = await _interviewRoundRepository.UpdateAsync(interviewRound);
            return _mapper.Map<InterviewRoundDTO>(updatedInterviewRound);
        }
    }
}
