using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class ResumeDAO
    {
        private static ResumeDAO instance;
        public static ResumeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResumeDAO();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<Resume>> GetAllAsync()
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Resumes.Include(x=>x.Applications)
                    .Where(s => !s.IsDelete).ToListAsync();
            }
        }

        public async Task<Resume> GetByIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Resumes.Include(x => x.Applications).FirstOrDefaultAsync(j => j.Id == id && !j.IsDelete);
            }
        }

        public async Task<IEnumerable<Resume>> GetByIdUserAsync(long idUser)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.Resumes.Include(x => x.Applications)
                    .Where(s => !s.IsDelete && s.UserId == idUser).ToListAsync();
            }
        }

        public async Task<Resume> AddAsync(Resume resume)
        {
            using (var _context = new RecuitmentDbContext())
            {
                
                _context.Resumes.Add(resume);
                await _context.SaveChangesAsync();
                return resume;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var resume = await _context.Resumes.FindAsync(id);
                if (resume == null)
                {
                    return false;
                }

                resume.IsDelete = true;
                _context.Entry(resume).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }


    }
}
