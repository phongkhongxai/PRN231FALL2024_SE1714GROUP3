using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class InterviewRoundDAO
    {
        private static InterviewRoundDAO instance;
        public static InterviewRoundDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InterviewRoundDAO();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<InterviewRound>> GetAllAsync()
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewRounds
                    .Include(ir => ir.Job)
                    .Where(ir => !ir.IsDelete) 
                    .ToListAsync();
            }
        }

        public async Task<InterviewRound> GetByIdAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewRounds
                    .Include(ir => ir.Job)   
                    .FirstOrDefaultAsync(ir => ir.Id == id && !ir.IsDelete);
            }
        }

        public async Task<IEnumerable<InterviewRound>> FindAsync(Expression<Func<InterviewRound, bool>> predicate)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewRounds
                    .Include(ir => ir.Job)   
                    .Where(predicate)
                    .ToListAsync();
            }
        }

        public async Task<InterviewRound> AddAsync(InterviewRound interviewRound)
        {
            using (var _context = new RecuitmentDbContext())
            {
                _context.InterviewRounds.Add(interviewRound);
                await _context.SaveChangesAsync();
                return interviewRound;
            }
        }

        public async Task<InterviewRound> UpdateAsync(InterviewRound interviewRound)
        {
            using (var _context = new RecuitmentDbContext())
            {
                _context.Entry(interviewRound).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return interviewRound;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using (var _context = new RecuitmentDbContext())
            {
                var interviewRound = await _context.InterviewRounds.FindAsync(id);
                if (interviewRound == null)
                {
                    return false;
                }

                interviewRound.IsDelete = true;   
                _context.Entry(interviewRound).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<IEnumerable<InterviewRound>> GetInterviewRoundsByJobIdAsync(long jobId)
        {
            using (var _context = new RecuitmentDbContext())
            {
                return await _context.InterviewRounds
                                .Where(ir => ir.JobId == jobId && !ir.IsDelete)
                                .ToListAsync();
            } 
        }
    }
}
