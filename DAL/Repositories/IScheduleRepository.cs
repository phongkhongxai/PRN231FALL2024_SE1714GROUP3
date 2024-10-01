using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IScheduleRepository
    {
        public Task<List<Schedule>> GetAllSchedules();
        public Task<Schedule> GetScheduleById(long id);
        public Task<Schedule> AddSchedule(Schedule schedule);
        public Task<Schedule> UpdateSchedule(Schedule schedule);
        public Task<bool> DeleteSchedule(long id);
    }
}
