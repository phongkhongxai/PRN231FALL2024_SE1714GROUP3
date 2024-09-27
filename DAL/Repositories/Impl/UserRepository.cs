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
        public async Task<List<User>> GetAllUsers()=> await UserDAO.Instance.GetAllUsers();
        public async Task<bool> DeleteUser(long id)=> await UserDAO.Instance.DeleteUser(id);
        public async Task<User> UpdateUser(User user)=> await UserDAO.Instance.UpdateUser(user);
    }
}
