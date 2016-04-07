using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class WorkorderController : BaseController
    {
        [GET("Workorder/GetWorkorders")]
        public ActionResult GetWorkorders()
        {
            var json = JsonHelper.GetObjects("getWorkorders.json"); 
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/CreateWorkOrder")]
        public ActionResult CreateWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Workorder/SaveWorkOrder")]
        public ActionResult SaveWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}