using System.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class WorkorderController : Controller
    {
        public JsonResult GetWorkorders()
        {
            var json = JsonHelper.GetObjects("getWorkorders.json"); 
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}