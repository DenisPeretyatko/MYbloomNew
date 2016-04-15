using System.Collections.Generic;
using System.Web.Mvc;
using BloomService.Web.Infrastructure;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ICallTypeSageApiService _callTypeSageApiService;
        private readonly IEmployeeSageApiService _employeeSageApiService;
        private readonly IEquipmentSageApiService _equipmentSageApiService;
        private readonly ILocationSageApiService _locationSageApiService;
        private readonly IProblemSageApiService _problemSageApiService;

        public DashboardController(ICallTypeSageApiService callTypeSageApiService, IEmployeeSageApiService employeeSageApiService, IEquipmentSageApiService equipmentSageApiService,
            ILocationSageApiService locationSageApiService, IProblemSageApiService problemSageApiService)
        {
            _callTypeSageApiService = callTypeSageApiService;
            _employeeSageApiService = employeeSageApiService;
            _equipmentSageApiService = equipmentSageApiService;
            _locationSageApiService = locationSageApiService;
            _problemSageApiService = problemSageApiService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [GET("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = new LookupsModel();
            lookups.Locations = _locationSageApiService.Get();

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard")]
        public ActionResult GetDashboard()
        {
            var json = JsonHelper.GetObjects("getDashboard.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard/SendNotification")]
        public ActionResult SendNotification()
        {
            var json = JsonHelper.GetObjects("getNotifications.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}