using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gender_api.Models;

namespace Gender_api.DataAccessLayer.Library
{
    public static class UserService
    {
        public static bool CreateUser(CreateUserModel data)
        {
            using (var db = new GenderEnt())
            {
                if (!(from a in db.users where a.email == data.Email select a.id).Any())
                {
                    db.users.Add(new users
                                 {
                                     company = data.Company,
                                     created = DateTime.Now,
                                     email = data.Email,
                                     firstname = data.Firstname,
                                     lastname = data.Lastname,
                                     password = data.Password
                                 });

                    db.SaveChanges();
                    
                    return true;
                }
                return false;
            }
        }
    }
}