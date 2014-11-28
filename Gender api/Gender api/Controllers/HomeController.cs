using System;
using System.Text;
using System.Web.Mvc;
using BusinessLayer.Account;
using Gender_api.BusinessLayer;
using Gender_api.DataAccessLayer;
using Gender_api.DataAccessLayer.Library;
using Gender_api.Models;
using MySql.Data.MySqlClient.Properties;
using Newtonsoft.Json;

namespace Gender_api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserService.CreateUser(model))
                {
                    AuthenticationService.SignIn(model.Email, true, AuthenticationService.UserRoles.Member);

                    return RedirectToAction("Dashboard");
                }
            }

            ModelState.AddModelError("Email", "Email allready in use");
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authUser = AuthenticationService.GeAuthenticationResponse(model.Email,model.Password, model.RememberMe);

                if (authUser.Authenticated)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }

            ModelState.AddModelError("Email", "Wrong email or password");
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public string Test()
        {
            var salt = BCrypt.GenerateSalt();
            return BCrypt.HashPassword("lol123", salt);
        }

        /*
        public string AddMales()
        {
            using (var db = new GenderEnt())
            {
                string jsonObject = System.IO.File.ReadAllText(@"C:\Users\Mikkel\Desktop\females.json", Encoding.UTF8);

                dynamic names = JsonConvert.DeserializeObject(jsonObject);

                var count = 0;

                foreach (var d in names)
                {
                    int place = d.Placering;
                    int number = d.Antal;
                    string name = d.Navn;

                    if (!name.Contains(","))
                    {
                        db.names.Add(new names
                                     {
                                         gender = 1,
                                         name = name,
                                         number = number
                                     });

                        count++;
                    }
                    else
                    {
                        string[] arr = name.Split(',');

                        foreach (var s in arr)
                        {
                            db.names.Add(new names
                                         {
                                             gender = 1,
                                             name = s,
                                             number = number
                                         });

                            count++;
                        }
                    }
                }

                db.SaveChanges();
                return count.ToString();
            }
            
        }
         */
        
    }
}
