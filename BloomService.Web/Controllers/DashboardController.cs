using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using BloomService.Web.Infrastructure.Dependecy;
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
            dashboard.WorkOrders = workorders.OrderByDescending(x => x.ScheduleDate);
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
                Date = DateTime.Now.GetLocalDate(),
                Time = DateTime.UtcNow.GetLocalDate().TimeOfDay
            };

            var stringDate = String.Format("{0} {1}", notificationTime.Date.Date.ToString("dd-MM-yyyy"), notificationTime.Time.ToString(@"hh\:mm\:ss"));
            
            return Json(stringDate, JsonRequestBehavior.AllowGet);
        }

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
            lookups.NotificationTime = String.Format("{0} {1}", notificationTime.Date.Date.ToString("dd-MM-yyyy"), notificationTime.Time.ToString(@"hh\:mm\:ss"));

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }
        
    }
}