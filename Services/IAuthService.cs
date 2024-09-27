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
        AuthDTO Authenticate(string emailOrUsername, string password);
        string GenerateJwtToken(User user);
        UserDTO SignUp(UserDTO user);
    }
}
