using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        [GET("Schedule")]
        public ActionResult GetSchedules()
        {
            var json = JsonHelper.GetObjects("getSchedule.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}