using BusinessObjects.DTO;
using BusinessObjects.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class UserController : ODataController
    {
        private IUserService userService;
        public UserController (IUserService userService)
        {
            this.userService = userService;
        }
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] long id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UserUpdateDTO userUpdate)
        {
            var updatedUser = await userService.UpdateUser(id, userUpdate);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            var success = await userService.DeleteUser(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword([FromRoute] long id, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var success = await userService.ChangePassword(id, changePasswordDTO);

            if (!success)
            {
                return BadRequest("Current password is incorrect or user not found.");
            }
            return NoContent();
        }
    }
}
