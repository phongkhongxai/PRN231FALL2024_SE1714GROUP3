using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class JobDAO
    {
        private static JobDAO instance;
        public static JobDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JobDAO();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Jobs
                    .Include(j => j.User)
                    .Include(j => j.Applications)
                    .Include(j => j.JobSkills)
                        .ThenInclude(js => js.Skill)
                    .Include(j => j.InterviewRounds) 
                    .Where(j => !j.IsDelete)
                    .ToListAsync();
            }
        }

        public async Task<Job> GetByIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Jobs
                    .Include(j => j.User)
                    .Include(j => j.Applications)
                    .Include(j => j.JobSkills)
                        .ThenInclude(js => js.Skill)
                    .Include(j => j.InterviewRounds)   
                    .FirstOrDefaultAsync(j => j.Id == id && !j.IsDelete);
            }
        }

        public async Task<IEnumerable<Job>> FindAsync(Expression<Func<Job, bool>> predicate)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Jobs
                    .Include(j => j.User)
                    .Include(j => j.Applications)
                    .Include(j => j.JobSkills)
                    .Include(j => j.InterviewRounds)  // Include InterviewRounds
                    .Where(predicate)
                    .ToListAsync();
            }
        }

        public async Task<Job> AddAsync(Job job)
        {
            using (var _context = new RecuitmentDbContext())
            {
                job.CreatedDate = DateTime.UtcNow;
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return job;
            }
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            using (var _context = new RecuitmentDbContext())
            {
                _context.Entry(job).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return job;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var job = await _context.Jobs.FindAsync(id);
                if (job == null)
                {
                    return false;
                }

                job.IsDelete = true;
                _context.Entry(job).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> AddSkillToJobAsync(long jobId, long skillId, string? experiences = null)
        {
            using (var _context = new RecuitmentDbContext())
            {
                // Kiểm tra Job có tồn tại hay không
                var job = await _context.Jobs
                    .Include(j => j.JobSkills)
                    .FirstOrDefaultAsync(j => j.Id == jobId && !j.IsDelete);
                if (job == null)
                {
                    throw new KeyNotFoundException("Job not found.");
                }

                // Kiểm tra Skill có tồn tại hay không
                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillId && !s.IsDelete);
                if (skill == null)
                {
                    throw new KeyNotFoundException("Skill not found.");
                }

                // Kiểm tra Skill đã có trong Job hay chưa
                if (job.JobSkills.Any(js => js.SkillId == skillId))
                {
                    throw new InvalidOperationException("Skill already exists in the job.");
                }

                // Thêm JobSkill vào Job
                var jobSkill = new JobSkill
                {
                    JobId = job.Id,
                    SkillId = skill.Id,
                    Experiences = experiences
                };

                job.JobSkills.Add(jobSkill);

                // Cập nhật Job và lưu thay đổi vào cơ sở dữ liệu
                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> RemoveSkillFromJobAsync(long jobId, long skillId)
        {
            using (var _context = new RecuitmentDbContext())
            { 
                var job = await _context.Jobs
                    .Include(j => j.JobSkills)
                    .FirstOrDefaultAsync(j => j.Id == jobId && !j.IsDelete);
                if (job == null)
                {
                    throw new KeyNotFoundException("Job not found.");
                }
                 
                var jobSkill = job.JobSkills.FirstOrDefault(js => js.SkillId == skillId);
                if (jobSkill == null)
                {
                    throw new InvalidOperationException("Skill does not exist in the job.");
                }
                 
                job.JobSkills.Remove(jobSkill); 
                _context.JobSkills.Remove(jobSkill);
                 
                await _context.SaveChangesAsync();

                return true;
            }
        } 

        public async Task<bool> AddInterviewRoundAsync(long jobId, InterviewRound interviewRound)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var job = await _context.Jobs
                    .Include(j => j.InterviewRounds)
                    .FirstOrDefaultAsync(j => j.Id == jobId && !j.IsDelete);
                if (job == null)
                {
                    throw new KeyNotFoundException("Job not found.");
                }

                interviewRound.JobId = jobId; // Set the JobId for the interview round
                job.InterviewRounds.Add(interviewRound);

                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> DeleteInterviewRoundAsync(long jobId, long interviewRoundId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var job = await _context.Jobs
                    .Include(j => j.InterviewRounds)
                    .FirstOrDefaultAsync(j => j.Id == jobId && !j.IsDelete);
                if (job == null)
                {
                    throw new KeyNotFoundException("Job not found.");
                }

                var interviewRound = await _context.InterviewRounds.FindAsync(interviewRoundId);
                if (interviewRound == null || interviewRound.JobId != jobId)
                {
                    throw new KeyNotFoundException("Interview round not found.");
                }

                _context.InterviewRounds.Remove(interviewRound);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
