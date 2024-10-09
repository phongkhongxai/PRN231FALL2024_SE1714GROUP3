using BusinessObjects.Entity;
using DAL.DbContenxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class InterviewRoundRepository:IInterviewRoundRepository
    {  
        public async Task<IEnumerable<InterviewRound>> GetAllAsync()
        {
            return await InterviewRoundDAO.Instance.GetAllAsync();
        }

        public async Task<InterviewRound> GetByIdAsync(long id)
        {
            return await InterviewRoundDAO.Instance.GetByIdAsync(id);
        }

        public async Task<InterviewRound> AddAsync(InterviewRound interviewRound)
        {
            return await InterviewRoundDAO.Instance.AddAsync(interviewRound);
        }

        public async Task<InterviewRound> UpdateAsync(InterviewRound interviewRound)
        { 
            return await InterviewRoundDAO.Instance.UpdateAsync(interviewRound);
        }

        public async Task<bool> DeleteAsync(long id)
        {  
            return await InterviewRoundDAO.Instance.DeleteAsync(id);
        } 

        public async Task<IEnumerable<InterviewRound>> GetInterviewRoundsByJobIdAsync(long jobId)
        {
            return await InterviewRoundDAO.Instance.GetInterviewRoundsByJobIdAsync(jobId);
        }
    }
}
