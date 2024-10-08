using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class ScheduleDAO

    {
        private static ScheduleDAO instance;

        public static ScheduleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScheduleDAO();
                }
                return instance;
            }
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            var db = new RecuitmentDbContext();
            return await db.Schedules.Include(c=>c.User).ToListAsync();
        }

        public async Task<Schedule> GetScheduleById(long id)
        {
            var db = new RecuitmentDbContext();
            return await db.Schedules.Include(c=>c.Interviews).FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);
        }
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            var db = new RecuitmentDbContext();
            db.Schedules.AddAsync(schedule);
            await db.SaveChangesAsync();
            return schedule;
        }
        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            var db = new RecuitmentDbContext();
            db.Entry(schedule).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return schedule;
        }
        public async Task<bool> DeleteSchedule (long id)
        {
            var db = new RecuitmentDbContext();
            var schedule = await db.Schedules.FindAsync(id);
            if (schedule == null) return false;
            schedule.IsDelete = true;
            db.Entry(schedule).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
    }
}
