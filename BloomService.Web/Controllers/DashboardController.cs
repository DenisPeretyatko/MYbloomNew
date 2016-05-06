namespace BloomService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Infrastructure.Constants;
    using BloomService.Web.Infrastructure.Hubs;
    using BloomService.Web.Models;
    using BloomService.Web.Services.Abstract;

    public class DashboardController : BaseController
    {
        private readonly ICallTypeService _callTypeService;

        private readonly ICustomerService _customerService;

        private readonly IEmployeeService _employeeService;

        private readonly IEquipmentService _equipmentService;

        private readonly ILocationService _locationService;

        private readonly IProblemService _problemService;

        private readonly IRepairService _repairService;

        public DashboardController(
            ICallTypeService callTypeService, 
            IEmployeeService employeeService, 
            IEquipmentService equipmentService, 
            ILocationService locationService, 
            IProblemService problemService, 
            ICustomerService customerService, 
            IRepairService repairService)
        {
            _callTypeService = callTypeService;
            _employeeService = employeeService;
            _equipmentService = equipmentService;
            _locationService = locationService;
            _problemService = problemService;
            _customerService = customerService;
            _repairService = repairService;
        }

        [GET("Dashboard")]
        public ActionResult GetDashboard()
        {
            var json = JsonHelper.GetObjects("getDashboard.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = new LookupsModel();
            var locations = _locationService.Get();
            var calltypes = _callTypeService.Get();
            var problems = _problemService.Get();
            var employes = _employeeService.Get();
            var equipment = _equipmentService.Get();
            var customer = _customerService.Get();
            var repairs = _repairService.Get();

            lookups.Locations = AutoMapper.Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = AutoMapper.Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = AutoMapper.Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = AutoMapper.Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = AutoMapper.Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = AutoMapper.Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.Hours = AutoMapper.Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs.ToList());
            lookups.RateSheets = RateSheets.RateSheetsList;
            lookups.PaymentMethods = PaymentMethod.PaymentMethodList;

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [GET("Dashboard/SendNotification")]
        public ActionResult SendNotification()
        {
            var json = JsonHelper.GetObjects("getNotifications.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}