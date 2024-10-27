using BusinessObjects.DTO;
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
    public class ApllicationController : ODataController
    {
        private readonly IApplicationService applicationService;

        public ApllicationController (IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplication()
        {
            var apps = await applicationService.GetAllApplicationsAsync();
            return Ok(apps);
        }


        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<ApplicationDTO>> GetApplicationById([FromRoute] long id)
        {
            var app = await applicationService.GetApplicationByIdAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        [HttpGet("job/{id}")]
        [EnableQuery]
        public async Task<ActionResult<ApplicationDTO>> GetApplicationByJobId([FromRoute] long id)
        {
            var app = await applicationService.GetApplicationByJobIdAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        [HttpGet("user/{id}")]
        [EnableQuery]
        public async Task<ActionResult<ApplicationDTO>> GetApplicationByUserId([FromRoute] long id)
        {
            var app = await applicationService.GetApplicationByUserIdAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        [HttpPost]
        public async Task<ActionResult<JobDTO>> CreateApp([FromBody] ApplicationCreateDTO applicationCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdApp = await applicationService.CreateApplicationAsync(applicationCreateDTO);

            return Created(new Uri($"/odata/Applications({createdApp.Id})", UriKind.Relative), createdApp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApp([FromRoute] long id, [FromBody] ApplicationUpdateDTO appUpdate)
        {
            var updatedApp = await applicationService.UpdateApplicationAsync(id, appUpdate);
            if (updatedApp == null)
            {
                return NotFound();
            }

            return Ok(updatedApp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp([FromRoute] long id)
        {
            var success = await applicationService.DeleteApplicationAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
