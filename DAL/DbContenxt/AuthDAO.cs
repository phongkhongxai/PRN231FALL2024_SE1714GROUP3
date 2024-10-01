using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public class AuthDAO
    {
        private static AuthDAO instance;
        public static AuthDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AuthDAO();
                }
                return instance;
            }
        }

        public async Task<User> GetUserByEmailOrUsername(string emailOrUsername)
        {
            var db = new RecuitmentDbContext();
            return await db.Users.FirstOrDefaultAsync(c => c.Email == emailOrUsername || c.Username == emailOrUsername);
        }

        public void CreateUser(User user)
        {
            var db = new RecuitmentDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
