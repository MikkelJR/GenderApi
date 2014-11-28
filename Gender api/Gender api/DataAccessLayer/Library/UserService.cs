using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Account;
using Gender_api.Models;

namespace Gender_api.DataAccessLayer.Library
{
    public static class UserService
    {
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static bool CreateUser(CreateUserModel data)
        {
            using (var db = new GenderEnt())
            {
                if (!(from a in db.users.AsNoTracking() where a.email == data.Email select a.id).Any())
                {
                    db.users.Add(new users
                                 {
                                     company = data.Company,
                                     created = DateTime.Now,
                                     email = data.Email,
                                     firstname = data.Firstname,
                                     lastname = data.Lastname,
                                     password = BCrypt.HashPassword(data.Password, BCrypt.GenerateSalt())
                                 });

                    db.SaveChanges();
                    
                    return true;
                }
                return false;
            }
        }

        public static IEnumerable<SelectListItem> FillUserDropDownList()
        {
            using (var db = new GenderEnt())
            {
                return (from a in db.users.AsNoTracking()
                    select new SelectListItem
                           {
                               Selected = false,
                               Text = a.email,
                               Value = a.id.ToString()
                           }).ToList();
            }
        }
        

        /// <summary>
        /// Check if email number exist.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static bool EmailExist(string email)
        {
            using (var db = new GenderEnt())
            {
                return (from a in db.users.AsNoTracking() where a.email.Equals(email) select a.id).Any();
            }
        }

        /// <summary>
        /// Retrieves the temporary user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static TempUser RetrieveTempUser(string email)
        {
            using (var db = new GenderEnt())
            {
                var getUser = (from a in db.users.AsNoTracking()
                               where a.email.Equals(email)
                               select new TempUser
                               {
                                   Email = a.email,
                                   Password = a.password
                               }).SingleOrDefault();

                return getUser;
            }
        }

        public class TempUser
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        

        
    }
}