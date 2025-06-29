﻿using BusinessObjects.DTO;
using BusinessObjects.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Recuitment_Group3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IUserService userService, IEmailService emailService)
        {
            _authService = authService;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            var authResult = await _authService.Authenticate(loginRequest.EmailOrUsername, loginRequest.Password);
            if (!authResult.IsAuthenticated)
            {
                return Unauthorized(new { Message = authResult.Message });
            }

            return Ok(authResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Signup([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = await _authService.SignUp(userDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];
                token = token.Split(' ')[1];
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GDWwIm+XjcfazEAD0TNwLtC+nNr1CU5F8fC4hy2mZAI=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                var userIdClaim = claimsPrincipal.FindFirst("Id");
                if (HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    HttpContext.Request.Headers.Remove("Authorization");
                }
                return Ok(new AuthDTO
                {
                    IsAuthenticated = true,
                    Message = "Logout successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthDTO
                {
                    IsAuthenticated = false,
                    Message = "Something go wrong" + ex.Message
                });
            }
        }

        [HttpPost("fotgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgot)
        {
            var user = await _userService.FindByEmail(forgot.Email);
            if (user == null)
            {
                return BadRequest(new
                {
                    message = "User not found"
                });
            }
            try
            {
                await _emailService.SendPasswordResetTokenAsync(forgot.Email);
                return Ok(new
                {
                    message = "Password reset token sent to your email."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO reset)
        {
            if (reset == null)
            {
                return BadRequest("Reset password data is required.");
            }
            try
            {
                await _emailService.ResetPasswordAsync(reset.Email, reset.Token, reset.NewPassword);
                return Ok("Password reset successfully");
            } catch (Exception ex)
            {
                return BadRequest($"Error resetting password: {ex.Message}");
            }
        }
    }
}
