using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IInterviewRoundRepository
    {
        Task<IEnumerable<InterviewRound>> GetAllAsync();
        Task<InterviewRound> GetByIdAsync(long id);
        Task<InterviewRound> AddAsync(InterviewRound interviewRound);
        Task<InterviewRound> UpdateAsync(InterviewRound interviewRound);
        Task<bool> DeleteAsync(long id);
        //Task<IEnumerable<InterviewRound>> FindAsync(Expression<Func<InterviewRound, bool>> predicate);
        Task<IEnumerable<InterviewRound>> GetInterviewRoundsByJobIdAsync(long jobId);
    }
}
