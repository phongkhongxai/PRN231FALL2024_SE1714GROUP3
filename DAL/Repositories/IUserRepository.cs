using BusinessObjects.DTO;
using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long id);
        Task<List<User>> GetAllUsers();
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(long id);

    }
}
