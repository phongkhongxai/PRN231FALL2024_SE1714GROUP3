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
    }
}
