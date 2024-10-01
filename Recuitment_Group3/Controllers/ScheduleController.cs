using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Impl;

namespace Recuitment_Group3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private IScheduleService scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetAllSchedules()
        {
            var schedules = await scheduleService.GetAllSchedules();
            return Ok(schedules);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDTO>> GetScheduleById([FromRoute] long id)
        {
            var schedule = await scheduleService.GetScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleDTO>> CreateSchedule([FromBody] ScheduleCreateDTO scheduleCreateDTO)
        {
            var schedule = await scheduleService.CreateSchedule(scheduleCreateDTO);
            if(schedule == null)
            {
                return BadRequest("Cannot add");
            }
            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule([FromRoute] long id)
        {
            var success = await scheduleService.DeleteSchedule(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
