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
            var workOrders = _repository.SearchFor<SageWorkOrder>().Open();
            foreach (var item in workOrders)
            {
                result.Add(new MapViewModel()
                {
                    WorkOrder = item,
                    DateEntered = item.ScheduleDate,
                    Color = item?.Color,
                    Employee = item?.EmployeeId
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