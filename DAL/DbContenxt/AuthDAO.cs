using BusinessObjects.Entity;
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

        public User GetUserByEmailOrUsername(string emailOrUsername)
        {
            var db = new RecuitmentDbContext();
            return db.Users.FirstOrDefault(c => c.Email == emailOrUsername || c.Username == emailOrUsername);
        }

        public void CreateUser(User user)
        {
            var db = new RecuitmentDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
