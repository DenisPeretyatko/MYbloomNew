using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLookups()
        {
            var json = JsonHelper.GetObjects("getLookups.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}