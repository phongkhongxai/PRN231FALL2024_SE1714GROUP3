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
        public static User GetUserById(long id)
        {
            using var db = new RecuitmentDbContext();
            return db.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id && !u.IsDelete);
        }
    }
}
