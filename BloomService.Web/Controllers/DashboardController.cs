﻿using System.Web.Mvc;
using BloomService.Web.Infrastructure;
using AttributeRouting.Web.Mvc;

namespace BloomService.Web.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [GET("Dashboard/GetLookups")]
        public ActionResult GetLookups()
        {
            var json = JsonHelper.GetObjects("getLookups.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard/GetDashboard")]
        public ActionResult GetDashboard()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}