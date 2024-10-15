using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;
using Services.Impl;

namespace Recuitment_Group3.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class InterviewSessionsController : ODataController
    {
        private readonly IInterviewSessionService _interviewSessionService; 

        public InterviewSessionsController(IInterviewSessionService interviewSessionService)
        {
            _interviewSessionService = interviewSessionService; 
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<InterviewSessionDTO>>> GetAllSessions()
        {
            var sessions = await _interviewSessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }


        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<InterviewSessionDTO>> GetSessionById([FromRoute] long id)
        {
            var job = await _interviewSessionService.GetSessionByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<InterviewSessionDTO>> CreateSession([FromBody] InterviewSessionCreateDTO interviewSessionCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdSession = await _interviewSessionService.CreateSessionAsync(interviewSessionCreateDTO);

            return CreatedAtAction(nameof(GetSessionById), new { key = createdSession.Id }, createdSession); 
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession([FromRoute] long id, [FromBody] InterviewSessionUpdateDTO interviewSessionUpdateDTO)
        {
            var updatedSession = await _interviewSessionService.UpdateSessionAsync(id, interviewSessionUpdateDTO);
            if (updatedSession == null)
            {
                return NotFound();
            }

            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession([FromRoute] long id)
        {
            var success = await _interviewSessionService.DeleteSessionAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
