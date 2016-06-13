namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public abstract class BaseController : Controller
    {
        protected ActionResult Success()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult Error(string message)
        {
            return Json(new { success = false, message }, JsonRequestBehavior.AllowGet);
        }
    }
}