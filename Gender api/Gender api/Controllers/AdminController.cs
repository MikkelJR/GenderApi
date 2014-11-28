using System.Web.Mvc;
using Gender_api.DataAccessLayer.Library;
using Gender_api.Models;

namespace Gender_api.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateKey()
        {
            var model = new CreateKeyModel
                        {
                            Items = UserService.FillUserDropDownList(),
                            KeyLimit = 1000
                        };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateKey(CreateKeyModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ViewKey",
                    new {id = KeyService.CreateKey(model.SelectedItemId, model.KeyLimit)});
            }

            model.Items = UserService.FillUserDropDownList();
            return View(model);
        }

        public ActionResult ViewKey(int id)
        {
            return View(KeyService.GetKeyInfo(id));
        }
    }
}