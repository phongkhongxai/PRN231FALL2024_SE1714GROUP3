using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Recuitment_Group3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IInterviewSessionService _interviewSessionService;

        public ScheduleController(IInterviewSessionService interviewSessionService)
        {
            _interviewSessionService = interviewSessionService;
        }
         
        [HttpGet("interviewer/{interviewerId}")]
        public async Task<ActionResult<IEnumerable<InterviewerScheduleDTO>>> GetScheduleForInterviewer(long interviewerId)
        {
            var schedule = await _interviewSessionService.GetScheduleForInterviewerAsync(interviewerId);

            if (schedule == null)
            {
                return NotFound(new { Message = "No schedule found for the interviewer." });
            }

            return Ok(schedule);
        }
         
        [HttpGet("applicant/{applicationId}")]
        public async Task<ActionResult<IEnumerable<ApplicantScheduleDTO>>> GetScheduleForApplicant(long applicationId)
        {
            var schedule = await _interviewSessionService.GetScheduleForApplicantAsync(applicationId);

            if (schedule == null)
            {
                return NotFound(new { Message = "No schedule found for the applicant." });
            }

            return Ok(schedule);
        }
    }
}
