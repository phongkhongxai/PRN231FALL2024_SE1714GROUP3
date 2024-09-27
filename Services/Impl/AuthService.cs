using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task<AuthDTO> Authenticate(string emailOrUsername, string password)
        {
            var user = await _authRepository.GetUserByEmailOrUsername(emailOrUsername);
            if (user == null || user.Password != HashPassword(password)) 
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

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public UserDTO SignUp(UserDTO userDTO)
        {
            var existingUser = _authRepository.GetUserByEmailOrUsername(userDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email or username.");
            }

            var hashPassword = HashPassword(userDTO.Password);

            var newUser = new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = hashPassword,
                Phone = "default" ?? userDTO.Phone,
                Address = "default" ?? userDTO.Address,
                Gender = "default" ?? userDTO.Gender,
                RoleId = 1,
                IsDelete = false
            };

            _authRepository.CreateUser(newUser);

            return new UserDTO
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
                Phone = newUser.Phone,
                Address = newUser.Address,
                Gender = newUser.Gender,
                RoleId = newUser.RoleId
            };
        }
    }
}
