using System;
using System.Linq;
using BloomService.Web.Models;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using Domain.Entities.Concrete;
    using System.Collections.Generic;

    using Infrastructure.Mongo;
    using Infrastructure.Queries;

    public class LocationController : BaseController
    {
        private readonly IRepository _repository;

        public LocationController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Location")]
        public ActionResult GetLocations(MapModel model)
        {
            var result = new List<MapViewModel>();
            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open" && x.AssignmentId == 0).ToList();
            var tmpLocations = workOrders.Select(x => x.Location);
            var itemLocations = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Name)).ToList();
            var assignments = _repository.SearchFor<SageAssignment>(x => !string.IsNullOrEmpty(x.Employee)).OrderByDescending(x => x.ScheduleDate).ThenByDescending(x => x.StartTime).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList();
            foreach (var item in workOrders)
            {
                var itemLocation = itemLocations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation == null) continue;
                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;

                var assignment = assignments.FirstOrDefault(x => x.WorkOrder == item.WorkOrder);
                if (string.IsNullOrEmpty(assignment?.Employee)) continue;
                var employee = employees.SingleOrDefault(x => x.Employee == assignment.EmployeeId);

                result.Add(new MapViewModel
                {
                    WorkOrder = item,
                    DateEntered = assignment.ScheduleDate,
                    Color = employee?.Color,
                    Employee = assignment != null ? assignment.EmployeeId : 0
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var utcDateLimit = DateTime.UtcNow.AddHours(-1);
            var techLocations = _repository.SearchFor<SageTechnicianLocation>(x => x.Date >= utcDateLimit);
            var employees = new List<SageEmployee>();

            foreach (var location in techLocations)
            {
                var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == location.Employee).FirstOrDefault();
                if (employee == null) continue;
                employee.Latitude = location.Latitude;
                employee.Longitude = location.Longitude;
                employees.Add(employee);
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}