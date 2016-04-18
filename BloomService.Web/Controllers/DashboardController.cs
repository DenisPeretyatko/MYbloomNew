using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Web.Infrastructure;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure.Constants;
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
        private readonly ICustomerSageApiService _customerSageApiService;

        public DashboardController(ICallTypeSageApiService callTypeSageApiService, IEmployeeSageApiService employeeSageApiService, IEquipmentSageApiService equipmentSageApiService,
            ILocationSageApiService locationSageApiService, IProblemSageApiService problemSageApiService, ICustomerSageApiService customerSageApiService)
        {
            _callTypeSageApiService = callTypeSageApiService;
            _employeeSageApiService = employeeSageApiService;
            _equipmentSageApiService = equipmentSageApiService;
            _locationSageApiService = locationSageApiService;
            _problemSageApiService = problemSageApiService;
            _customerSageApiService = customerSageApiService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [GET("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = new LookupsModel();
            var locations = _locationSageApiService.Get();
            var calltypes = _callTypeSageApiService.Get();
            var problems = _problemSageApiService.Get();
            var employes = _employeeSageApiService.Get();
            var equipment = _equipmentSageApiService.Get();
            var customer = _customerSageApiService.Get();

            lookups.Locations = AutoMapper.Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = AutoMapper.Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = AutoMapper.Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = AutoMapper.Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = AutoMapper.Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = AutoMapper.Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.RateSheets = RateSheets.RateSheetsList;
            lookups.Hours = EstimateRepairHours.EstimateRepairHoursList;
            lookups.PaymentMethods = PaymentMethod.PaymentMethodList;

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