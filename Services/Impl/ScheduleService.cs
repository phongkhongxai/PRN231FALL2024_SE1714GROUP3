using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
        }
        public async Task<ScheduleDTO> CreateSchedule(ScheduleCreateDTO scheduleCreateDTO)
        {
            var scheduleMap = mapper.Map<Schedule>(scheduleCreateDTO);
            var schedule = await scheduleRepository.AddSchedule(scheduleMap);
            if (schedule == null)
            {
                throw new Exception("Schedule creation failed.");
            }
            return mapper.Map<ScheduleDTO>(schedule);

        }

        public async Task<bool> DeleteSchedule(long id)
        {
            var schedule =  await scheduleRepository.GetScheduleById(id);
            if(schedule == null)
            {
                return false;
            }
            return await scheduleRepository.DeleteSchedule(id);
        }

        public async Task<List<ScheduleDTO>> GetAllSchedules()
        {
            var schedules = await scheduleRepository.GetAllSchedules();
            
            return mapper.Map<List<ScheduleDTO>>(schedules);
        }

        public async Task<ScheduleDTO> GetScheduleById(long id)
        {
            var job = await scheduleRepository.GetScheduleById(id);
            return mapper.Map<ScheduleDTO>(job);
        }
    }
}
