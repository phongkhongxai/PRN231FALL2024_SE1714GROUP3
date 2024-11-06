using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class ApplicationDAO
    {
        private static ApplicationDAO instance;
        public static ApplicationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationDAO();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Applications
                    .Include(c => c.User)
                    .Include(c => c.Resume)
                    .Include(c => c.Job)
        .               ThenInclude(j => j.InterviewRounds)
                    .Where(c => !c.IsDelete)
                    .ToListAsync();
            }
        }

        public async Task<Application> GetByIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Applications
                    .Include(c => c.User)
                    .Include(c => c.Resume)
                    .Include(c => c.Job)
                         .ThenInclude(j => j.InterviewRounds)
                     .FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);
            }
        }
        public async Task<IEnumerable<Application>> GetByUserIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Applications
                    .Include(c => c.User)
                    .Include(c => c.Resume)
                    .Include(c => c.Job)
                    .Include(c => c.Job)
                        .ThenInclude(j => j.InterviewRounds)
                    .Where(c => c.UserId == id && !c.IsDelete).ToListAsync();
            }
        }

        public async Task<IEnumerable<Application>> GetByJobIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Applications
                    .Include(c => c.User)
                    .Include(c => c.Resume)
                    .Include(c => c.Job)
                    .Include(c => c.Job)
                        .ThenInclude(j => j.InterviewRounds)
                    .Where(c => c.JobId == id && !c.IsDelete).ToListAsync();
            }
        }

        public async Task<Application> AddAsync(Application application)
        {
            using (var _context = new RecuitmentDbContext())
            {
                application.IsDelete = false;
                application.Status = "PENDING";
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return application;
            }
        }

        public async Task<Application> UpdateAsync(Application application)
        {
            using (var _context = new RecuitmentDbContext())
            {
                _context.Entry(application).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return application;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var application = await _context.Applications.FindAsync(id);
                if (application == null)
                {
                    return false;
                }

                application.IsDelete = true;
                _context.Entry(application).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
