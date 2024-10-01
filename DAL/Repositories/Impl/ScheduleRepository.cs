using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class ScheduleRepository : IScheduleRepository
    {
        public Task<Schedule> AddSchedule(Schedule schedule) => ScheduleDAO.Instance.AddSchedule(schedule);

        public Task<bool> DeleteSchedule(long id) => ScheduleDAO.Instance.DeleteSchedule(id);

        public Task<List<Schedule>> GetAllSchedules()=> ScheduleDAO.Instance.GetAllSchedules();

        public Task<Schedule> GetScheduleById(long id) => ScheduleDAO.Instance.GetScheduleById(id);

        public Task<Schedule> UpdateSchedule(Schedule schedule) => ScheduleDAO.Instance.UpdateSchedule(schedule);
    }
}
