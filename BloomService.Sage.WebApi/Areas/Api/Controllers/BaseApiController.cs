using System.Web.Mvc;

namespace Sage.WebApi.Areas.Api.Controllers
{
    public class BaseApiController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public new JsonResult Json(object obj)
        {
            var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}