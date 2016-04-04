using System.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class LocationController : Controller
    {
        public JsonResult GetLocations()
        {
            var json = JsonHelper.GetObjects("getLocations.json"); 
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}