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
            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open");
            foreach (var item in workOrders)
            {
                var itemLocation = _repository.SearchFor<SageLocation>(l => l.Name == item.Location).FirstOrDefault();
                if (itemLocation == null) continue;
                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;

                var assignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == item.WorkOrder ).OrderByDescending(x => x.ScheduleDate).ThenByDescending(x => x.StartTime).FirstOrDefault();

                if (string.IsNullOrEmpty(assignment?.Employee) || item.AssignmentId != 0) continue;
                result.Add(new MapViewModel()
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