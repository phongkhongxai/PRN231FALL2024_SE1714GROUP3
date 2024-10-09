using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(long id); 
        Task<Job> CreateJobAsync(Job job);
        Task<Job> UpdateJobAsync(Job job);
        Task<bool> DeleteAsync(long id);
        Task<bool> AddSkillToJobAsync(long jobId, long skillId, string? experiences = null);
        Task<bool> RemoveSkillFromJobAsync(long jobId, long skillId );

        Task<bool> AddInterviewRoundAsync(long jobId, InterviewRound interviewRound);
        Task<bool> DeleteInterviewRoundAsync(long jobId, long interviewRoundId);
    }
}
