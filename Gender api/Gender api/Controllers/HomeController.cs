using System;
using System.Text;
using System.Web.Mvc;
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
                    return View();
                }
                
            }

            ModelState.AddModelError("Email", "Email allerede i brug");
            return View(model);
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
