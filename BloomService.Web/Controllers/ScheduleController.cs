using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        [GET("Schedule/GetSchedules")]
        public ActionResult GetSchedules()
        {
            var json = JsonHelper.GetObjects("getSchedule.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}