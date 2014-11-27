using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gender_api.DataAccessLayer.Library;

namespace Gender_api.Controllers
{
    public class RequestController : Controller
    {
        [HttpGet]
        public JsonResult Single(string name, string key)
        {
            if (!KeyService.VerifyKey(key)) return Json("Invalid key", JsonRequestBehavior.AllowGet);
            if (!KeyService.RequestLimit(key)) return Json("Your request limit have been reached", JsonRequestBehavior.AllowGet);

            KeyService.AddRequest(key);
            RequestService.CreateRequest(key, name, HttpContext.Request.UserHostAddress);
            return Json(GetGender.GetSingleGender(name), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Bulk(string names, string key)
        {
            if (!names.Contains(",")) return Json("Forkert format", JsonRequestBehavior.AllowGet);
            if (!KeyService.VerifyKey(key)) return Json("Invalid key", JsonRequestBehavior.AllowGet);
            if (!KeyService.RequestLimit(key)) return Json("Your request limit have been reached", JsonRequestBehavior.AllowGet);

            KeyService.AddRequest(key);
            RequestService.CreateRequest(key, names, HttpContext.Request.UserHostAddress);
            List<string> nameList = names.Split(',').ToList();
            return Json(GetGender.BulkGender(nameList), JsonRequestBehavior.AllowGet);
        }
	}
}