using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class AuthRepository : IAuthRepository
    {
        public void CreateUser(User user)
        {
            AuthDAO.Instance.CreateUser(user);
        }

        public Task<User> GetUserByEmailOrUsername(string emailOrUsername)
        {
            return AuthDAO.Instance.GetUserByEmailOrUsername(emailOrUsername);
        }
    }
}
