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
        public Task<bool> ChangePassword(long id, string currentPass, string newPass) => UserDAO.Instance.ChangePassword(id, currentPass, newPass);
        public Task<User> FindByEmail(string email)
        {
            return UserDAO.Instance.FindByEmail(email);
        }
        public Task<bool> AddUserSkill(long id, long skillId, string? ex) => UserDAO.Instance.AddSkillForUser(id, skillId,ex);
        public async Task<bool> RemoveSkill(long userId, long skillId) => await UserDAO.Instance.RemoveSkill(userId, skillId);

        public Task<User> CreateUser(User user)
        {
            return UserDAO.Instance.CreateUser(user);
        }
    }
}
