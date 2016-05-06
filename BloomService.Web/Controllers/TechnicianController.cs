namespace BloomService.Web.Controllers
{
using System.Web.Mvc;

using AttributeRouting.Web.Mvc;

using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

    public class TechnicianController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public TechnicianController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [POST("Technician/AssignWorkOrder/{id}")]
        public ActionResult AssignWorkOrder()
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = _employeeService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/Save")]
        public ActionResult SaveTechniciance(TechnicianModel model)
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/SaveLocations/{id}")]
        public ActionResult SaveTecnitianLocations()
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/SaveSchedule/{id}")]
        public ActionResult SaveTecnitianSchedule()
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}