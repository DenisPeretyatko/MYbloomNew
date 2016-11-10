using System;
using System.Linq;
using BloomService.Web.Models;
using System.Web.Mvc;
using BloomService.Domain.Entities.Concrete;
using System.Collections.Generic;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure;
using BloomService.Web.Infrastructure.Mongo;

namespace BloomService.Web.Controllers
{
    public class LocationController : BaseController
    {
        private readonly IRepository _repository;
        private readonly BloomServiceConfiguration _settings;

        public LocationController(IRepository repository, BloomServiceConfiguration settings)
        {
            _repository = repository;
            _settings = settings;
        }

        [HttpPost]
        [Route("Location")]
        public ActionResult GetLocations(MapModel model)
        {
            var mapModels = new List<MapViewModel>();
            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open" && x.AssignmentId == 0).ToList();
            var tmpLocations = workOrders.Select(x => x.Location).Distinct();
            var woIds = workOrders.Select(x => x.WorkOrder).Distinct();
            var itemLocations = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Name)).ToList();
            var assignments = _repository.SearchFor<SageAssignment>(x => !string.IsNullOrEmpty(x.Employee) && woIds.Contains(x.WorkOrder)).OrderByDescending(x => x.ScheduleDate).ThenByDescending(x => x.StartTime).ToList();//12
            var employees = _repository.GetAll<SageEmployee>().ToList();
            foreach (var item in workOrders)
            {
                var itemLocation = itemLocations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation == null)
                    continue;

                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;

                var assignment = assignments.FirstOrDefault(x => x.WorkOrder == item.WorkOrder);
                if (string.IsNullOrEmpty(assignment?.Employee))
                {
                    mapModels.Add(new MapViewModel
                    {
                        WorkOrder = item
                    });
                    continue;
                }

                var employee = employees.SingleOrDefault(x => x.Employee == assignment.EmployeeId);

                mapModels.Add(new MapViewModel
                {
                    WorkOrder = item,
                    DateEntered = assignment.ScheduleDate,
                    Color = employee?.Color,
                    Employee = assignment.EmployeeId
                });
            }
            return Success(mapModels); 
        }

        [HttpGet]
        [Route("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var utcDateLimit = DateTime.Now.GetLocalDate(_settings.Timezone).AddHours(-1);
            var techLocations = _repository.SearchFor<SageTechnicianLocation>(x => x.Date >= utcDateLimit);
            var employees = new List<SageEmployee>();

            foreach (var location in techLocations)
            {
                var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == location.Employee).FirstOrDefault();
                if (employee == null)
                    continue;

                employee.Latitude = location.Latitude;
                employee.Longitude = location.Longitude;
                employees.Add(employee);
            }

            return Success(employees);
        }
    }
}