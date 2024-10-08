using AutoMapper;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace Recuitment_Group3.Controllers
{
      
    [Route("odata/[controller]")]
    [ApiController]
    public class SkillsController : ODataController
    {
        private readonly ISkillService _skillService; 

        public SkillsController(ISkillService skillService )
        {
            _skillService = skillService; 
        }

        // GET: odata/Skills
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        // GET: odata/Skills({id})
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<SkillDTO>> GetSkillById([FromRoute] long id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        // POST: odata/Skills
        [HttpPost]
        public async Task<ActionResult<SkillDTO>> CreateSkill([FromBody] SkillDTO skillCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            var createdSkill = await _skillService.CreateSkillAsync(skillCreateDto);
            return Created(new Uri($"/odata/Skills({createdSkill.Id})", UriKind.Relative), createdSkill);
        }

        // PUT: odata/Skills({id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] long id, [FromBody] SkillDTO skillUpdateDto)
        {
            var updatedSkill = await _skillService.UpdateSkillAsync(id, skillUpdateDto);
            if (updatedSkill == null)
            {
                return NotFound();
            }

            return Ok(updatedSkill);
        }

        // DELETE: odata/Skills({id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill([FromRoute] long id)
        {
            var success = await _skillService.DeleteSkillAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
