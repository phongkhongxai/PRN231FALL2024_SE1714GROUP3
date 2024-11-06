using AutoMapper;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace Recuitment_Group3.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class InterviewRoundsController : ODataController
    {
        private readonly IInterviewRoundService _interviewRoundService;
        private readonly IMapper _mapper;

        public InterviewRoundsController(IInterviewRoundService interviewRoundService, IMapper mapper)
        {
            _interviewRoundService = interviewRoundService;
            _mapper = mapper;
        }

         
        [HttpGet]
        [EnableQuery]  
        public async Task<ActionResult<IEnumerable<InterviewRoundDTO>>> GetAllInterviewRounds()
        {
            var interviewRounds = await _interviewRoundService.GetAllInterviewRoundsAsync();
            return Ok(interviewRounds);
        }

        /*[HttpGet("ByJobId({jobId})")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<InterviewRoundDTO>>> GetInterviewRoundsByJobId([FromODataUri] long jobId)
        {
            var interviewRounds = await _interviewRoundService.GetInterviewRoundsByJobIdAsync(jobId);
            if (interviewRounds == null || !interviewRounds.Any())
            {
                return NotFound();
            }

            return Ok(interviewRounds);
        }*/


        // GET: odata/InterviewRounds(5)
        [HttpGet("{key}")]
        [EnableQuery]
        public async Task<ActionResult<InterviewRoundDTO>> GetInterviewRoundById(long key)
        {
            var interviewRound = await _interviewRoundService.GetInterviewRoundByIdAsync(key);
            if (interviewRound == null)
            {
                return NotFound();
            }
            return Ok(interviewRound);
        }

        // POST: odata/InterviewRounds
        [HttpPost]
        public async Task<ActionResult<InterviewRoundDTO>> CreateInterviewRound([FromBody] InterviewRoundCreateDTO interviewRoundCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdInterviewRound = await _interviewRoundService.CreateInterviewRoundAsync(interviewRoundCreateDto);
            return Created(new Uri($"/odata/InterviewRounds({createdInterviewRound.Id})", UriKind.Relative), createdInterviewRound);

        }
         
        [HttpPut("{key}")]
        public async Task<ActionResult<InterviewRoundDTO>> UpdateInterviewRound(long key, [FromBody] InterviewRoundUpdateDTO interviewRoundUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedInterviewRound = await _interviewRoundService.UpdateInterviewRoundAsync(key, interviewRoundUpdateDto);
            if (updatedInterviewRound == null)
            {
                return NotFound();
            }

            return Ok(updatedInterviewRound);
        }

        [HttpPut("/status/{key}")]
        public async Task<ActionResult<InterviewRoundDTO>> UpdateInterviewRoundStatus(long key, [FromBody] string status)
        {
            try
            { 
                var updatedInterviewRound = await _interviewRoundService.UpdateStatusInterviewRoundAsync(key, status);
                 
                if (updatedInterviewRound == null)
                {
                    return NotFound($"Interview round with ID {key} was not found.");
                } 
                return Ok(updatedInterviewRound);
            }
            catch (ArgumentException ex)
            { 
                return BadRequest(ex.Message);
            }
        }


        // DELETE: odata/InterviewRounds(5)
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteInterviewRound(long key)
        {
            var success = await _interviewRoundService.DeleteInterviewRoundAsync(key);
            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Returns 204 No Content on successful deletion
        }
    }
}
