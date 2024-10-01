using BusinessObjects.DTO;
using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IScheduleService
    {
        public Task<List<ScheduleDTO>> GetAllSchedules();
        public Task<ScheduleDTO> GetScheduleById(long id);
        public Task<ScheduleDTO> CreateSchedule(ScheduleCreateDTO scheduleCreateDTO);
        public Task<bool> DeleteSchedule(long id);

    }
}
