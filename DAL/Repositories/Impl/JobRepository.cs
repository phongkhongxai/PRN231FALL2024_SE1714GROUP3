using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class JobRepository : IJobRepository
    {
        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await JobDAO.Instance.GetAllAsync();
        }

        public async Task<Job> GetJobByIdAsync(long id)
        {
            return await JobDAO.Instance.GetByIdAsync(id);
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            return await JobDAO.Instance.AddAsync(job);
        }

        public async Task<Job> UpdateJobAsync(Job job)
        {
            return await JobDAO.Instance.UpdateAsync(job);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await JobDAO.Instance.DeleteAsync(id);
        }
        //demo use findAsync
        public async Task<IEnumerable<Job>> GetHighPayingJobsAsync(long salaryThreshold)
        {
            return await JobDAO.Instance.FindAsync(j => j.Amount > salaryThreshold);
        }

        public async Task<bool> AddSkillToJobAsync(long jobId, long skillId, string? experiences = null)
        {
            return await JobDAO.Instance.AddSkillToJobAsync(jobId, skillId, experiences);
        }
        public async Task<bool> RemoveSkillFromJobAsync(long jobId, long skillId)
        {
            return await JobDAO.Instance.RemoveSkillFromJobAsync(jobId, skillId );
        }

        public async Task<bool> AddInterviewRoundAsync(long jobId, InterviewRound interviewRound)
        {
            return await JobDAO.Instance.AddInterviewRoundAsync(jobId, interviewRound);
        }

        public async Task<bool> DeleteInterviewRoundAsync(long jobId, long interviewRoundId)
        {
            return await JobDAO.Instance.DeleteInterviewRoundAsync(jobId, interviewRoundId);

        }
    }
}
