using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailOrUsername(string emailOrUsername);
        void CreateUser(User user);
    }
}
