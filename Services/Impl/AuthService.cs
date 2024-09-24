using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public AuthDTO Authenticate(string emailOrUsername, string password)
        {
            var user = _authRepository.GetUserByEmailOrUsername(emailOrUsername);
            if (user == null || user.Password != password) 
            {
                return new AuthDTO
                {
                    IsAuthenticated = false,
                    Message = "Invalid credentials"
                };
            }

            var token = GenerateJwtToken(user);
            return new AuthDTO
            {
                IsAuthenticated = true,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                Message = "Authentication successful"
            };
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
