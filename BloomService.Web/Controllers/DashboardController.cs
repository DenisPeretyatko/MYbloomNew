using System.Web.Mvc;
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

        [GET("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var json = JsonHelper.GetObjects("getLookups.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard")]
        public ActionResult GetDashboard()
        {
            var json = JsonHelper.GetObjects("getDashboard.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard/SendNotification")]
        public ActionResult SendNotification()
        {
            var json = JsonHelper.GetObjects("getNotifications.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}