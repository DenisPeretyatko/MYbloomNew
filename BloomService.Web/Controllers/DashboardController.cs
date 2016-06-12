using System;
using System.Globalization;

namespace BloomService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using AutoMapper;

    using Domain.Entities.Concrete;
    using Infrastructure.Constants;
    using Infrastructure.Hubs;
    using Models;
    using Services.Abstract;
    using Domain.Repositories.Abstract;

    public class DashboardController : BaseController
    {

        private readonly ILocationService locationService;

        private readonly IRepository _repository;

        public DashboardController( 
            ILocationService locationService,
            IRepository repository)
        {
            this.locationService = locationService;
            _repository = repository;
        }
        [GET("Dashboard")]
        [Authorize]
        public ActionResult GetDashboard()
        {
            var dashboard = new DashboardViewModel();
            var listWO = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open");
            var assignments = _repository.SearchFor<SageAssignment>(x => x.Employee == "");
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
            var locations = _repository.GetAll<SageLocation>();
            var calltypes = _repository.GetAll<SageCallType>();
            var problems = _repository.GetAll<SageProblem>();
            var employes = _repository.GetAll<SageEmployee>();
            var equipment = _repository.GetAll<SageEquipment>();
            var customer = _repository.GetAll<SageCustomer>();
            var repairs = _repository.GetAll<SageRepair>();

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