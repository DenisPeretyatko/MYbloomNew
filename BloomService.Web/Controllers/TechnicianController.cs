using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : BaseController
    {
        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var json = JsonHelper.GetObjects("getTechnicians.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/Save/{id}")]
        public ActionResult SaveTechniciance(long id)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/SaveSchedule/{id}")]
        public ActionResult SaveTecnitianSchedule()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [POST("Technician/SaveLocations/{id}")]
        public ActionResult SaveTecnitianLocations()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/AssignWorkOrder/{id}")]
        public ActionResult AssignWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}