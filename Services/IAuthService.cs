using BusinessObjects.DTO;
using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthService
    {
        Task<AuthDTO> Authenticate(string emailOrUsername, string password);
        string GenerateJwtToken(User user);
        Task<UserDTO> SignUp(UserDTO userDTO);
    }
}
