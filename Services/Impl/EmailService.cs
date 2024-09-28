using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;

        public EmailService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SendMailAsync(string email, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("khoabase170469@fpt.edu.vn", "avfc ignw xfez bazf"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("khoabase170469@fpt.edu.vn"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }

        private string GenerateResetToken(int length = 6)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4]; 
                rng.GetBytes(randomNumber);
                uint tokenValue = BitConverter.ToUInt32(randomNumber, 0) % (uint)Math.Pow(10, length); 
                return tokenValue.ToString("D" + length);
            }
        }


        public async Task SendPasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var token = GenerateResetToken();
            user.ResetPasswordToken = token;
            user.ResetPasswordExpiry = DateTime.UtcNow.AddMinutes(15);

            await _userRepository.UpdateUser(user);

            var subject = "Password Reset Request";
            var message = $"To reset your password, use the following token: <strong>{token}</strong>";

            await SendMailAsync(user.Email, subject, message);
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.ResetPasswordToken != token || user.ResetPasswordExpiry < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token");
            }

            user.Password = HashPassword(newPassword); 
            user.ResetPasswordToken = null;
            user.ResetPasswordExpiry = null;

            await _userRepository.UpdateUser(user);
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
    }
}
