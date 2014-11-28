using System;
using System.Web;
using System.Web.Security;
using BusinessLayer.Account;

namespace Gender_api.BusinessLayer
{
    public class AuthenticationResponse
    {
        public bool Authenticated { get; set; }

        public bool EmailExist { get; set; }
    }

    public static class AuthenticationService
    {
        public enum UserRoles
        {
            Member = 1,
            Moderator = 2,
            Admin = 3
        }

        /// <summary>
        /// Signs user in.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="persistent">if set to <c>true</c> [persistent].</param>
        /// <param name="role">The user role.</param>
        public static void SignIn(string userName, bool persistent, UserRoles role)
        {
            var authTicket = new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.Now,
                DateTime.Now.AddMinutes(60),
                persistent,
                role.ToString()
            );

            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Ges the authentication response.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns></returns>
        public static AuthenticationResponse GeAuthenticationResponse(string email, string password, bool rememberMe)
        {
            var newResponse = new AuthenticationResponse
                              {
                                  EmailExist = DataAccessLayer.Library.UserService.EmailExist(email)
                              };

            if (newResponse.EmailExist)
            {
                var tempUser = DataAccessLayer.Library.UserService.RetrieveTempUser(email);
                newResponse.EmailExist = true;

                if (BCrypt.CheckPassword(password, tempUser.Password))
                {
                    SignIn(tempUser.Email, rememberMe, UserRoles.Member);
                    newResponse.Authenticated = true;

                    return newResponse;
                }
            }

            newResponse.Authenticated = false;

            return newResponse;
        }

        /// <summary>
        /// Logs user out
        /// </summary>
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}