using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure;
using BloomService.Web.Models;
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

        [POST("Technician/Save")]
        public ActionResult SaveTechniciance(TechnicianModel model)
        {
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/SaveSchedule/{id}")]
        public ActionResult SaveTecnitianSchedule()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}