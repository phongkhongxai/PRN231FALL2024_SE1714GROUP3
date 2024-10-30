using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInterviewRoundService
    {
        
        Task<InterviewRoundDTO> GetInterviewRoundByIdAsync(long id);
        Task<InterviewRoundDTO> CreateInterviewRoundAsync(InterviewRoundCreateDTO interviewRoundCreateDto);
        Task<InterviewRoundDTO> UpdateInterviewRoundAsync(long id, InterviewRoundUpdateDTO interviewRoundUpdateDto);
        Task<bool> DeleteInterviewRoundAsync(long id);
        Task<IEnumerable<InterviewRoundDTO>> GetInterviewRoundsByJobIdAsync(long jobId);
        Task<IEnumerable<InterviewRoundDTO>> GetAllInterviewRoundsAsync();
        Task<InterviewRoundDTO> UpdateStatusInterviewRoundAsync(long id, string status);

    }
}
