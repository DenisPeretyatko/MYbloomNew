using System.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : Controller
    {
        public JsonResult GetTachnicians()
        {
            var json = JsonHelper.GetObjects("getTechnicians.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}