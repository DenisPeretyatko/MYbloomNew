using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class LocationController : BaseController
    {

        [GET("Location/GetLocations")]
        public ActionResult GetLocations()
        {
            var json = JsonHelper.GetObjects("getLocations.json"); 
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Location/GetTrucks")]
        public ActionResult GetTrucks()
        {
            var json = JsonHelper.GetObjects("getTrucks.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}