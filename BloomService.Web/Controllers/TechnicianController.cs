using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : BaseController
    {
        [GET("Technician/GetTechnicians")]
        public ActionResult GetTechnicians()
        {
            var json = JsonHelper.GetObjects("getTechnicians.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Technician/SaveTechniciance/{id}")]
        public ActionResult SaveTechniciance(long id)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/SaveTecnitianSchedule")]
        public ActionResult SaveTecnitianSchedule()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [POST("Technician/SaveTecnitianLocations")]
        public ActionResult SaveTecnitianLocations()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/AssignWorkOrder")]
        public ActionResult AssignWorkOrder()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}