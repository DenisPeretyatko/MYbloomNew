using System;
using BloomService.Web.Infrastructure.Dependecy;

namespace BloomService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using BloomService.Web.Infrastructure.Mongo;

    using Domain.Entities.Concrete;
    using Infrastructure.Constants;

    using Models;

    using Infrastructure.Queries;
    using Infrastructure.Services.Interfaces;

    public class DashboardController : BaseController
    {
        private readonly ILocationService locationService;
        private readonly IRepository _repository;
        private readonly IAuthorizationService _autorization;
        private readonly IDashboardService _dashboardService;
        private readonly INotificationService _notification;

        public DashboardController(
            ILocationService locationService,
            IRepository repository,
            IAuthorizationService autorization, IDashboardService dashboardService, INotificationService notification)
        {
            this.locationService = locationService;
            _repository = repository;
            _autorization = autorization;
            _dashboardService = dashboardService;
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
        public ActionResult GetDashboard(MapModel model)
        {
            var dashboard = new DashboardViewModel();
            var workorders = _repository.SearchFor<SageWorkOrder>().Open().ToList();
            var assignments = _repository.SearchFor<SageAssignment>(x => x.Employee == "").ToList();
            var chart = new List<ChartModel>();
            var chartModel = new ChartModel();

            var chartData = new List<Chart>
            {
                new Chart {label = "Open", data = workorders.Count(), color = "#000000"},
                new Chart {label = "Assigned", data = workorders.Count(x => assignments.Any(a => a.WorkOrder == x.WorkOrder)), color = "#1ab394"},
                new Chart {label = "Roof leak", data = workorders.Count(x => x.Problem == "Roof Leak"), color = "#df4242"},
                new Chart {label = "Closed today", data = workorders.Count(x => x.DateClosed == DateTime.Now.GetLocalDate().Date), color = "#30B335"}
            };
            chartModel.data = chartData;
            chart.Add(chartModel);

            dashboard.Chart = chart;
            var tmpLocations = workorders.Select(x => x.Location);
            var locations = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Name)).ToList();
            dashboard.WorkOrders = workorders.OrderByDescending(x => x.ScheduleDate);
            foreach (var order in dashboard.WorkOrders)
            {
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location != null)
                {
                    order.Latitude = location.Latitude;
                    order.Longitude = location.Longitude;
                    order.Address = string.Join(" ", String.Join(", ", new[] { location.Name, location.Address, location.City, location.State, location.ZIP }.Where(str => !string.IsNullOrEmpty(str))));
                }
            }

            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Dashboard/UpdateNotificationTime")]
        public ActionResult UpdateNotificationTime(string str = null)
        {
            var currentDate = DateTime.Now.GetLocalDate();
            _repository.Add(new SageUserProfile()
            {
                UserId = UserModel.Id,
                Date = currentDate,
                Time = currentDate.TimeOfDay
            });
            var notificationTime = _repository.GetAll<SageUserProfile>().LastOrDefault(x => x.UserId == UserModel.Id) ?? new SageUserProfile()
            {
                UserId = UserModel.Id,
                Date = currentDate,
                Time = currentDate.TimeOfDay
            };

            var stringDate = String.Format("{0} {1}", notificationTime.Date.Date.ToString(DateTimeFormat.DateFormat), notificationTime.Time.ToString(DateTimeFormat.TimeFormat));

            return Json(stringDate, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = _dashboardService.GetLookups();

            var notificationTime = _repository.GetAll<SageUserProfile>().LastOrDefault(x => x.UserId == UserModel.Id) ?? new SageUserProfile()
            {
                UserId = UserModel.Id,
                Date = DateTime.Now.GetLocalDate(),
                Time = DateTime.UtcNow.GetLocalDate().TimeOfDay
            };

            lookups.Notifications = _notification.GetLastNotifications();
            lookups.NotificationTime = String.Format("{0} {1}", notificationTime.Date.Date.ToString(DateTimeFormat.DateFormat), notificationTime.Time.ToString(DateTimeFormat.TimeFormat));

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [Route("Exception")]
        public ActionResult Exception()
        {
            throw new Exception("Exception");
        }
    }
}