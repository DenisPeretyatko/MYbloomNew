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
        private readonly ICallTypeService callTypeService;

        private readonly ICustomerService customerService;

        private readonly IEmployeeService employeeService;

        private readonly IEquipmentService equipmentService;

        private readonly ILocationService locationService;

        private readonly IProblemService problemService;

        private readonly IRepairService repairService;

        public DashboardController(
            ICallTypeService callTypeService, 
            IEmployeeService employeeService, 
            IEquipmentService equipmentService, 
            ILocationService locationService, 
            IProblemService problemService, 
            ICustomerService customerService, 
            IRepairService repairService)
        {
            this.callTypeService = callTypeService;
            this.employeeService = employeeService;
            this.equipmentService = equipmentService;
            this.locationService = locationService;
            this.problemService = problemService;
            this.customerService = customerService;
            this.repairService = repairService;
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
            var locations = locationService.Get();
            var calltypes = callTypeService.Get();
            var problems = problemService.Get();
            var employes = employeeService.Get();
            var equipment = equipmentService.Get();
            var customer = customerService.Get();
            var repairs = repairService.Get();

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