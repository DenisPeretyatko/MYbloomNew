using System.Linq;
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

        public LocationController(IWorkOrderService workOrderService, ILocationService locationService)
        {
            _workOrderService = workOrderService;
            _locationService = locationService;
        }

        [GET("Location")]
        public ActionResult GetLocations()
        {
            //var workOrders = _workOrderService.Get().Where(x => x.Status == "Open");
            //var locations = _locationService.Get();
            //foreach (var item in workOrders)
            //{
            //    var itemLocation = locations.FirstOrDefault(l => l.Name == item.Location);
            //    if (itemLocation != null)
            //    {
            //        item.Latitude = itemLocation.Latitude;
            //        item.Longitude = itemLocation.Longitude;
            //    }
            //}
            var workOrders = JsonHelper.GetObjects("getLocations.json");
            return Json(workOrders, JsonRequestBehavior.AllowGet);
        }

        [GET("Location/Trucks")]
        public ActionResult GetTrucks()
        {
            var json = JsonHelper.GetObjects("getTrucks.json");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}