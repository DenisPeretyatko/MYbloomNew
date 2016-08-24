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
            
            foreach (var item in workOrders)
            {
                var itemLocation = itemLocations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation == null) continue;
                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;

                var assignment = assignments.FirstOrDefault(x => x.WorkOrder == item.WorkOrder);
                if (string.IsNullOrEmpty(assignment?.Employee)) continue;
                result.Add(new MapViewModel
                {
                    WorkOrder = item,
                    DateEntered = assignment.ScheduleDate,
                    Color = assignment?.Color,
                    Employee = assignment != null ? assignment.EmployeeId : 0
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