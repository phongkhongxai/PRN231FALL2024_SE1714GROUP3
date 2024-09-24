using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserByIdAsync(long id)
        {
            return await UserDAO.Instance.GetUserById(id);
        }
    }
}
