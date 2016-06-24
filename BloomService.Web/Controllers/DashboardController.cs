using System;
using System.Globalization;
using BloomService.Web.Services.Concrete;
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

        public DashboardController( 
            ILocationService locationService,
            IRepository repository,
            IAuthorizationService autorization, IDashboardService dashboardService)
        {
            this.locationService = locationService;
            _repository = repository;
            _autorization = autorization;
            _dashboardService = dashboardService;
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
            
            return Json( new { access_token = token.Token }, JsonRequestBehavior.AllowGet);
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
            chartModel.data= chartData;
            chart.Add(chartModel);
            
            dashboard.Chart = chart;
            dashboard.WorkOrders = workorders;
            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Dashboard/Lookups")]
        public ActionResult GetLookups()
        {
            var lookups = new LookupsModel();
            lookups = _dashboardService.GetLookups();

            return Json(lookups, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Dashboard/SendNotification")]
        public ActionResult SendNotification()
        {
            var json = JsonHelper.GetObjects("getNotifications.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}