﻿using System.Drawing;
using System.Linq;
using BloomService.Web.Models;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using Domain.Entities.Concrete;
    using Infrastructure.Queries;
    using Domain.Extensions;
    using System.Collections.Generic;
    using System;
    using AutoMapper;

    using BloomService.Web.Infrastructure.Mongo;
    using BloomService.Web.Infrastructure.Services.Interfaces;

    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        private readonly IRepository _repository;

        public LocationController(ILocationService locationService, IRepository repository)
        {
            _locationService = locationService;
            _repository = repository;
        }

        [HttpPost]
        [Route("Location")]
        public ActionResult GetLocations(MapModel model)
        {
            var result = new List<MapViewModel>();
            var workOrders = new List<SageWorkOrder>();
            workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open"  ).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList();
            var locations = _repository.GetAll<SageLocation>().ToArray();
            foreach (var item in workOrders)
            {
                var itemLocation = locations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation == null)
                    continue;
                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;
               
                var assignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == item.WorkOrder).SingleOrDefault();

                if (string.IsNullOrEmpty(assignment?.Employee) || item.AssignmentId != null) continue;
                var tempEmployee = employees.FirstOrDefault(e => e.Name == assignment.Employee);
                var color = tempEmployee?.Color;
                var employee = tempEmployee?.Employee;
                result.Add(new MapViewModel()
                {
                    WorkOrder = item,
                    DateEntered = assignment.ScheduleDate,
                    Color = color,
                    Employee = employee
                });
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var employees = _repository.GetAll<SageEmployee>().ToList();
            var techLocations = _repository.GetAll<SageTechnicianLocation>();

            foreach (var employee in employees)
            {
                var itemLocation = techLocations.Where(x => x.Employee == employee.Employee).OrderByDescending(x => x.Date).FirstOrDefault();
                if (itemLocation == null)
                    continue;

                employee.Latitude = itemLocation.Latitude;
                employee.Longitude = itemLocation.Longitude;
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}