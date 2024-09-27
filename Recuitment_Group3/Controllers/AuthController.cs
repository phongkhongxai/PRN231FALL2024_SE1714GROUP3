using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace Recuitment_Group3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginRequest)
        {
            var authResult = _authService.Authenticate(loginRequest.EmailOrUsername, loginRequest.Password);
            if (!authResult.IsAuthenticated)
            {
                return Unauthorized(new { Message = authResult.Message });
            }

            return Ok(authResult);
        }

        [HttpPost("register")]
        public IActionResult Signup([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = _authService.SignUp(userDTO);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
