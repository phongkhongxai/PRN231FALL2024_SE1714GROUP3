using BusinessObjects.DTO;
using BusinessObjects.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Recuitment_Group3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ODataController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRefreshHandlerService _refreshHandler;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [NonAction]
        private string GenerateRefreshToken(User user)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshToken = Convert.ToBase64String(randomnumber);
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = user.Id,
                    TokenId = new Random().Next().ToString(),
                    RefreshTokenString = refreshToken,
                    ExpireAt = DateTime.Now.AddDays(7),
                    Statuses = ReStatuses.Enable
                };

                _refreshHandler.GenerateRefreshToken(refreshTokenEntity);
                return refreshToken;
            }
        }

        [HttpPost("RefreshAccessToken")]
        public async Task<ActionResult> RefreshAccessToken(TokenDTO token)
        {
            try
            {
                var jwtTokenHander = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GDWwIm+XjcfazEAD0TNwLtC+nNr1CU5F8fC4hy2mZAI=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                _refreshHandler.ResetRefreshToken();
                var tokenVerification = jwtTokenHander.ValidateToken(token.AccessTokenToken, tokenValidationParameters, out _);
                if (tokenVerification == null)
                {
                    return Ok(new AuthDTO
                    {
                        IsAuthenticated = false,
                        Message = "Invalid Param"
                    });
                }
                //check AccessToken expire?
                var epochTime = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                DateTimeOffset dateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(epochTime);
                DateTime dateTimeUtcConverted = dateTimeUtc.UtcDateTime;
                if (dateTimeUtcConverted > DateTime.UtcNow)
                {
                    return Ok(new AuthDTO
                    {
                        IsAuthenticated = false,
                        Message = "AccessToken had not expired",
                        Expiration = dateTimeUtcConverted
                    });
                }
                //check RefreshToken exist in DB
                var storedToken = _refreshHandler.GetRefreshToken(token.RefreshToken);
                //check RefreshToken is revoked?
                if (storedToken.Statuses == ReStatuses.Disable)
                {
                    return Ok(new AuthDTO
                    {
                        IsAuthenticated = false,
                        Message = "RefreshToken had been revoked"
                    });
                }
                var user = _userService.GetUserById(storedToken.UserId);
                //var newAt = GenerateToken(user, token.RefreshToken);

                return Ok(new AuthDTO
                {
                    IsAuthenticated = true,
                    Message = "Refresh AT success fully",
                    //Expiration = newAt
                });
            }
            catch (Exception ex)
            {
                return Ok(new AuthDTO
                {
                    IsAuthenticated = false,
                    Message = "Something go wrong"
                });
            }
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

        [HttpPost("Logout")]
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
                var userIdClaim = claimsPrincipal.FindFirst("EmployeeId");
                var refreshToken = _refreshHandler.GetRefreshTokenByEmployeeId(userIdClaim.Value);
                _refreshHandler.UpdateRefreshToken(refreshToken);
                _refreshHandler.ResetRefreshToken();
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
    }
}
