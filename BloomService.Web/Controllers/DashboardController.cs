using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Abstract;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Services.Concrete;
using Common.Logging;
using Newtonsoft.Json.Linq;

namespace BloomService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Domain.Entities.Concrete;
    using Infrastructure.Constants;
    using Infrastructure.Hubs;
    using Models;
    using Services.Abstract;
    using Domain.Repositories.Abstract;
    using Infrastructure.Queries;

    public class DashboardController : BaseController
    {
        private readonly ILocationService locationService;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly IRepository _repository;
        private readonly IAuthorizationService _autorization;
        private readonly INotificationService _notification;

        public DashboardController(
            ILocationService locationService,
            IRepository repository,
            IAuthorizationService autorization,
            INotificationService notification
           )
        {
            this.locationService = locationService;
            _repository = repository;
            _autorization = autorization;
            _notification = notification;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(string username, string password)
        {
            var token = _autorization.Authorization(username, password);
            if (token == null)
                return Error("Invalid user or password");

            return Json(new { access_token = token.Token }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Dashboard")]
        public ActionResult GetDashboard(MapViewModel model)
        {
            var dashboard = new DashboardViewModel();
            var workorders = _repository.SearchFor<SageWorkOrder>().Open().ToArray();
            var assignments = _repository.SearchFor<SageAssignment>(x => x.Employee == "").ToArray();
            var chart = new List<ChartModel>();
            var chartModel = new ChartModel();

            var chartData = new List<List<object>>
            {
                new List<object> {"Open", workorders.Count()},
                new List<object> {"Assigned", workorders.Count(x => assignments.Any(a => a.WorkOrder == x.WorkOrder))},
                new List<object> {"Roof leak", workorders.Count(x => x.Problem == "Roof leak")},
                new List<object> {"Closed today", workorders.Count(x => x.DateClosed == DateTime.Now)},
            };
            chartModel.data = chartData;
            chart.Add(chartModel);

            dashboard.Chart = chart;
            dashboard.WorkOrders = workorders;
            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Route("Dashboard/UpdateNotificationTime")]
        public ActionResult UpdateNotificationTime(string str = null)
        {
            _repository.Add(new SageUserProfile()
            {
                UserId = UserModel.Id,
                Date = DateTime.Today.Date.Date,
                Time = DateTime.Now.TimeOfDay
            });
            return Success();
        }

        [HttpGet]
        [Route("Dashboard/Lookups")]
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
            var ratesheets = _repository.GetAll<SageRateSheet>();
            var permissionCodes = _repository.GetAll<SagePermissionCode>();
            var parts = _repository.GetAll<SagePart>();
            var notificationTime = _repository.GetAll<SageUserProfile>().LastOrDefault(x => x.UserId == UserModel.Id) ?? new SageUserProfile() {
                UserId = UserModel.Id,
                Date = DateTime.Today.Date.Date,
                Time = DateTime.Now.TimeOfDay
            };
           
            
            lookups.Locations = Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.Hours = Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs.ToList());
            lookups.Notifications = _notification.GetLastNotifications();
            lookups.NotificationTime = String.Format("{0} {1}", notificationTime.Date.ToLocalTime().Date.ToString("dd-MM-yyyy"), notificationTime.Time.ToString(@"hh\:mm\:ss"));
         
            lookups.RateSheets = Mapper.Map<List<SageRateSheet>, List<RateSheetModel>>(ratesheets.ToList());
            lookups.PermissionCodes = Mapper.Map<List<SagePermissionCode>, List<PermissionCodeModel>>(permissionCodes.ToList());

            lookups.PaymentMethods = PaymentMethod.PaymentMethodList;
            lookups.Parts = Mapper.Map<List<SagePart>, List<PartModel>>(parts.ToList());

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }
        
    }
}