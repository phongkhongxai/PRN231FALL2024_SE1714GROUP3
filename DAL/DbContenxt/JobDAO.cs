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
            var _context = new RecuitmentDbContext(); 
            return await _context.Jobs
                .Include(j => j.User)             
                .Include(j => j.Applications)     
                .Include(j => j.JobSkills)
                .ThenInclude(js => js.Skill)
                .ToListAsync();
        }

        public async Task<Job> GetByIdAsync(long id)
        {
            var _context = new RecuitmentDbContext();

            return await _context.Jobs
                .Include(j => j.User)
                .Include(j => j.Applications)
                .Include(j => j.JobSkills)
                .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.Id == id && !j.IsDelete);
        }

        public async Task<IEnumerable<Job>> FindAsync(Expression<Func<Job, bool>> predicate)
        {
            var _context = new RecuitmentDbContext();

            return await _context.Jobs
                .Include(j => j.User)
                .Include(j => j.Applications)
                .Include(j => j.JobSkills)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Job> AddAsync(Job job)
        {
            var _context = new RecuitmentDbContext();

            job.CreatedDate = DateTime.UtcNow;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            var _context = new RecuitmentDbContext();

            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var _context = new RecuitmentDbContext(); 
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

        public async Task<bool> AddSkillToJobAsync(long jobId, long skillId, string? experiences = null)
        {
            var _context = new RecuitmentDbContext();

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

}
