using System.Linq;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Repositories.Abstract;
    using Domain.Entities.Concrete;
    using Infrastructure.Extensions;

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

            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open" && x.Employee != "");                    
            var locations = _repository.GetAll<SageLocation>();
            foreach (var item in workOrders)
            {
                var itemLocation = locations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation != null)
                {
                    item.Latitude = itemLocation.Latitude;
                    item.Longitude = itemLocation.Longitude;
                }
            }
            return Json(workOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var employees = _repository.GetAll<SageEmployee>();
            var techLocations = _repository.GetAll<SageTechnicianLocation>();

            foreach (var item in employees)
            {
                var itemLocation = techLocations.Where(tl => tl.Employee == item.Employee).OrderByDescending(x => x.Date).FirstOrDefault();
                if (itemLocation != null)
                {
                    item.Latitude = itemLocation.Latitude;
                    item.Longitude = itemLocation.Longitude;
                }
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}