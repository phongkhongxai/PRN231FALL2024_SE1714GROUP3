using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;
using Services.Impl;

namespace Recuitment_Group3.Controllers
{

    [Authorize]
    [Route("odata/[controller]")]
    [ApiController]
    public class JobsController : ODataController
    {
        private IJobService jobService;
        
        public JobsController(IJobService jobService)
        {
            this.jobService = jobService;
        }
        [HttpGet]
        [EnableQuery]  
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllJob()
        {
            var jobs = await jobService.GetAllJobsAsync();
            return Ok(jobs);
        }

         
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<JobDTO>> GetJobById([FromRoute] long id)
        {
            var job = await jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<JobDTO>> CreateJob([FromBody] JobCreateDTO jobCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            var createdJob = await jobService.CreateJobAsync(jobCreateDto);  
            
            return Created(new Uri($"/odata/Jobs({createdJob.Id})", UriKind.Relative), createdJob);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob([FromRoute] long id, [FromBody] JobUpdateDTO jobUpdate)
        { 
            var updatedJob = await jobService.UpdateJobAsync(id, jobUpdate);
            if (updatedJob == null)
            {
                return NotFound();
            }

            return Ok(updatedJob);
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] long id)
        {
            var success = await jobService.DeleteJobAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
