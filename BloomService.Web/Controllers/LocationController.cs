using System.Linq;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using BloomService.Web.Infrastructure.Hubs;

    public class LocationController : BaseController
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly ILocationService _locationService;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IWorkOrderService workOrderService, ILocationService locationService, IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            _workOrderService = workOrderService;
            _locationService = locationService;
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
        }

        [GET("Location")]
        public ActionResult GetLocations()
        {
            var workOrders = _workOrderService.Get().Where(x => x.Status == "Open");
            var locations = _unitOfWork.Locations.Get();
            foreach (var item in workOrders)
            {
                var itemLocation = locations.FirstOrDefault(l => l.Name == item.Location);
                if (itemLocation != null)
                {
                    item.Latitude = itemLocation.Latitude;
                    item.Longitude = itemLocation.Longitude;
                }
            }
            //var workOrders = JsonHelper.GetObjects("getLocations.json");
            return Json(workOrders, JsonRequestBehavior.AllowGet);
        }

        [GET("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var employees = _employeeService.Get();
            var locations = _unitOfWork.Locations.Get();

            foreach (var item in employees)
            {
                var itemLocation = locations.FirstOrDefault(l => l.Employee == item.Name);
                if (itemLocation != null)
                {
                    item.Latitude = itemLocation.Latitude;
                    item.Longitude = itemLocation.Longitude;
                }
            }
            //var json = JsonHelper.GetObjects("getTrucks.json");
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}