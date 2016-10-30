using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.Queries;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Models;

namespace BloomService.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IRepository _repository;
        private readonly IAuthorizationService _autorization;
        private readonly IDashboardService _dashboardService;
        private readonly INotificationService _notification;
        private readonly BloomServiceConfiguration _settings;

        public DashboardController(
            IRepository repository,
            IAuthorizationService autorization,
            IDashboardService dashboardService,
            INotificationService notification,
            BloomServiceConfiguration settings)
        {
            _repository = repository;
            _autorization = autorization;
            _dashboardService = dashboardService;
            _notification = notification;
            _settings = settings;
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

            return Success(new { access_token = token.Token });
        }

        [HttpPost]
        [Route("Dashboard")]
        public ActionResult GetDashboard(MapModel model)
        {
            var dashboardModel = new DashboardViewModel();
            var workorders = _repository.SearchFor<SageWorkOrder>().Open().ToArray();
            var assignments = _repository.SearchFor<SageAssignment>(x => x.Employee == "").ToArray();
            var chart = new List<ChartModel>();
            var chartModel = new ChartModel();

            var chartData = new List<Chart>
            {
                new Chart {label = "Open", data = workorders.Count(), color = ChartColors.Open},
                new Chart {label = "Assigned", data = workorders.Count(x => assignments.Any(a => a.WorkOrder == x.WorkOrder)), color = ChartColors.Assigned},
                new Chart {label = "Roof leak", data = workorders.Count(x => x.Problem == "Roof Leak"), color = ChartColors.RoofLeak},
                new Chart {label = "Closed today", data = workorders.Count(x => x.DateClosed == DateTime.Now.GetLocalDate(_settings.CurrentTimezone).Date), color = ChartColors.ClosedToday}
            };
            chartModel.data = chartData;
            chart.Add(chartModel);

            dashboardModel.Chart = chart;
            var workoderLocation = workorders.Select(x => x.Location);
            var locations = _repository.SearchFor<SageLocation>(x => workoderLocation.Contains(x.Name)).ToArray();

            dashboardModel.WorkOrders = workorders.OrderByDescending(x => x.ScheduleDate);
            foreach (var order in dashboardModel.WorkOrders)
            {
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location != null)
                {
                    order.Latitude = location.Latitude;
                    order.Longitude = location.Longitude;
                    order.Address = string.Join(" ", string.Join(", ", new[]
                        { location.Name, location.Address, location.City, location.State, location.ZIP }
                        .Where(str => !string.IsNullOrEmpty(str))));
                }
            }

            return Success(dashboardModel);
        }

        [HttpPost]
        [Route("Dashboard/UpdateNotificationTime")]
        public ActionResult UpdateNotificationTime(string str = null)
        {
            var currentDate = DateTime.Now.GetLocalDate(_settings.CurrentTimezone);
            _repository.Add(new SageUserProfile()
            {
                UserId = UserModel.Id,
                Date = currentDate,
                Time = currentDate.TimeOfDay
            });

            var userProfile = _repository.GetAll<SageUserProfile>()
                .LastOrDefault(x => x.UserId == UserModel.Id) ??
                new SageUserProfile()
                {
                    UserId = UserModel.Id,
                    Date = currentDate,
                    Time = currentDate.TimeOfDay
                };

            var notificationTime = $"{userProfile.Date.Date.ToString(DateTimeFormat.DateFormat)} {userProfile.Time.ToString(DateTimeFormat.TimeFormat)}";
            return Success(notificationTime);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = _dashboardService.GetLookups();

            var userProfile = _repository.GetAll<SageUserProfile>()
                .LastOrDefault(x => x.UserId == UserModel.Id) ?? new SageUserProfile()
                {
                    UserId = UserModel.Id,
                    Date = DateTime.Now.GetLocalDate(_settings.CurrentTimezone),
                    Time = DateTime.Now.GetLocalDate(_settings.CurrentTimezone).TimeOfDay
                };

            lookups.Notifications = _notification.GetLastNotifications();
            lookups.NotificationTime = $"{userProfile.Date.Date.ToString(DateTimeFormat.DateFormat)} {userProfile.Time.ToString(DateTimeFormat.TimeFormat)}";

            return Success(lookups);
        }

        [AllowAnonymous]
        [Route("Exception")]
        public ActionResult Exception()
        {
            throw new ApplicationException("Test exception");
        }
    }
}