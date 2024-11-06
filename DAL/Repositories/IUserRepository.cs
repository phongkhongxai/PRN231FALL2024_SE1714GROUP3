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
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(long id);
        Task<User> FindByEmail(string email);
        Task<bool> ChangePassword(long id, string currentPass, string newPass);
        Task<bool> AddUserSkill(long id, long skillId, string? ex);
        Task<bool> RemoveSkill(long userId, long skillId);

    }
}
