using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmailService
    {
        Task SendMailAsync(string email, string subject, string body);
        Task SendPasswordResetTokenAsync(string email);
        Task ResetPasswordAsync(string email, string token, string newPassword);

    }
}
