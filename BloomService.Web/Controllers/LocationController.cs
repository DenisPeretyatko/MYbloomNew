using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Models;
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

        [POST("Location")]
        public ActionResult GetLocations(MapViewModel model)
        {
            var date = model.DateWorkOrder;
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            //date.Date.ToString("yy-MM-dd")
            var workOrders = _workOrderService.Get().Where(x => x.Status == "Open" && x.Employee != "");
                    
            
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
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}