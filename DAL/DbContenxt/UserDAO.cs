using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class UserDAO
    {
        private static UserDAO instance;
        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance; 
            }
        }
        public async Task<User> GetUserById(long id)
        {
            using var db = new RecuitmentDbContext();
            return db.Users.Include(u => u.Role)
                .Include(u => u.Resumes)
                .Include(u => u.Applications)
                .Include(u => u.Schedules)
                .Include(u => u.UserSkills)
                .FirstOrDefault(u => u.Id == id && !u.IsDelete);
        }
        public async Task<List<User>> GetAllUsers()
        {
            using var db = new RecuitmentDbContext();
            return await db.Users.Include(u => u.Role)
                .Include(u => u.Resumes)
                .Include(u => u.Applications)
                .Include(u => u.Schedules)
                .Include(u => u.UserSkills)
                .Where(u => !u.IsDelete).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            using var db = new RecuitmentDbContext();
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var db = new RecuitmentDbContext();
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
                user.IsDelete = true;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
            return true;
        }

        public async Task<User> FindByEmail(string email)
        {
            var db = new RecuitmentDbContext();
            return await db.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

    }
}
