using System;
using System.Globalization;

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

        private readonly IWorkOrderService workOrderService;

        private readonly IAssignmentService assignmentService;

        public DashboardController(
            ICallTypeService callTypeService, 
            IEmployeeService employeeService, 
            IEquipmentService equipmentService, 
            ILocationService locationService, 
            IProblemService problemService, 
            ICustomerService customerService, 
            IRepairService repairService, 
            IWorkOrderService workOrderService, IAssignmentService assignmentService)
        {
            this.callTypeService = callTypeService;
            this.employeeService = employeeService;
            this.equipmentService = equipmentService;
            this.locationService = locationService;
            this.problemService = problemService;
            this.customerService = customerService;
            this.repairService = repairService;
            this.workOrderService = workOrderService;
            this.assignmentService = assignmentService;
        }
        [GET("Dashboard")]
        [Authorize]
        public ActionResult GetDashboard()
        {
            var dashboard = new DashboardViewModel();
            var listWO = workOrderService.Get().Where(x => x.Status == "Open");
            var assignments = assignmentService.Get().Where(x => x.Employee == "");
            var chart = new List<ChartModel>();
            var chartModel = new ChartModel();
            
            var chartData = new List<List<object>>
            {
                new List<object> {"Open", listWO.Count()},
                new List<object> {"Assigned", listWO.Count(x => assignments.Any(a => a.WorkOrder == x.WorkOrder))},
                new List<object> {"Roof leak", listWO.Count(x => x.Problem == "Roof leak")},
                new List<object> {"Closed today", listWO.Count(x => x.DateClosed == DateTime.Now.ToString("yyyy-MM-dd"))},
            };
            chartModel.data= chartData;
            chart.Add(chartModel);
            
            dashboard.Chart = chart;
            dashboard.WorkOrders = listWO;
            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }

        [GET("Dashboard/Lookups")]
        [Authorize]
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

            lookups.Locations = Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.Hours = Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs.ToList());
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