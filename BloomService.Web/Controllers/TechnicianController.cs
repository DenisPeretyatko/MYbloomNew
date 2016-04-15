using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : BaseController
    {
        private readonly IEmployeeSageApiService _employeeSageApiService;

        public TechnicianController(IEmployeeSageApiService employeeSageApiService)
        {
            _employeeSageApiService = employeeSageApiService;
        }

        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = _employeeSageApiService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
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