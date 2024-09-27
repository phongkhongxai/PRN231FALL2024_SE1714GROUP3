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
                .ToListAsync();
        }

        public async Task<Job> GetByIdAsync(long id)
        {
            var _context = new RecuitmentDbContext();

            return await _context.Jobs
                .Include(j => j.User)
                .Include(j => j.Applications)
                .Include(j => j.JobSkills)
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
    }

}
