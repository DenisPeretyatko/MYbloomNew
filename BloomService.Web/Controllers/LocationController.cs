using System.Linq;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Repositories.Abstract;
    using Domain.Entities.Concrete;
    using Infrastructure.Queries;
    using Domain.Extensions;
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
        public ActionResult GetLocations(MapViewModel model)
        {
            var date = model.DateWorkOrder.NowIfMin();
            var temp = date.Date;
            var dateForSearch = temp.AddDays(1);
            var testWorkOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == "8051").Single();

            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ForDate(dateForSearch);
            var locations = _repository.GetAll<SageLocation>().ToArray();
            foreach (var item in workOrders)
            {
                var itemLocation = locations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation == null)
                    continue;

                item.Latitude = itemLocation.Latitude;
                item.Longitude = itemLocation.Longitude;
            }
            return Json(workOrders, JsonRequestBehavior.AllowGet);
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