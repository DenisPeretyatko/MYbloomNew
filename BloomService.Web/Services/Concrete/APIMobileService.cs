using System;

namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;
    using Models.Request;
    using System.Configuration;
    using Domain.Exceptions;
    using Domain.Extensions;

    public class ApiMobileService : IApiMobileService
    {
        private readonly IEmployeeService employeeService;

        private readonly IImageService imageService;

        private readonly IUnitOfWork unitOfWork;

        private readonly IUserService userService;

        private readonly IWorkOrderService workOrderService;

        private readonly BloomServiceConfiguration _settings;

        public ApiMobileService(
            IWorkOrderService workOrderService,
            IUserService userService,
            IEmployeeService employeeService,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            BloomServiceConfiguration bloomConfiguration)
        {
            this.imageService = imageService;
            this.workOrderService = workOrderService;
            this.userService = userService;
            this.employeeService = employeeService;
            this.unitOfWork = unitOfWork;
            _settings = bloomConfiguration;
        }

        public bool AddImage(ImageRequest model)
        {
            var pathToImage = HostingEnvironment.MapPath("/Images/");
            var workOrder = unitOfWork.GetEntities<SageWorkOrder>().SearchFor(x => x.WorkOrder == model.IdWorkOrder);
            if (workOrder == null)
            {
                return false;
            }

            var imagesDB = unitOfWork.GetEntities<SageImageWorkOrder>().SearchFor(x => x.WorkOrder == model.IdWorkOrder).FirstOrDefault();
            var countImage = 0;
            if (imagesDB != null && imagesDB.Images != null)
            {
                countImage = imagesDB.Images.Count();
            }
            else
            {
                imagesDB = new SageImageWorkOrder { Images = new List<ImageLocation>(), WorkOrder = model.IdWorkOrder };
            }

            var name = model.IdWorkOrder + "id" + countImage;
            var fileName = imageService.SaveFile(model.Image, pathToImage, name);
            var image = new ImageLocation()
            {
                Image = fileName,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
            imagesDB.Images.Add(image);
            countImage++;
            unitOfWork.GetEntities<SageImageWorkOrder>().Add(imagesDB);
            return true;
        }

        public IEnumerable<SageWorkOrder> GetWorkOreders()
        {
            var userId = userService.Name;
            var workOrders = unitOfWork.GetEntities<SageWorkOrder>().Get().Where(x=>x.Status=="Open").ToList();
            var result = workOrders.Where(x => x.Employee == userId);
            var locations = unitOfWork.GetEntities<SageLocation>().Get();
            foreach (var order in result)
            {
                order.Equipments = new List<SageEquipment>();
                var images = unitOfWork.GetEntities<SageImageWorkOrder>().SearchFor(x => x.WorkOrder == order.WorkOrder).FirstOrDefault();
                if (images != null)
                {
                    foreach(var image in images.Images)
                    {
                        image.Image = _settings.BSUrl + "/Images/" + image.Image;
                    }
                    order.Images = images.Images;
                }
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location == null)
                    continue;
                order.Latitude = location.Latitude;
                order.Longitude = location.Longitude;
                if(order.Equipment!=0)
                {
                    var equipments = unitOfWork.GetEntities<SageEquipment>().SearchFor(x => x.Equipment == order.Equipment.ToString());
                    order.Equipments.AddRange(equipments);
                }
            }
            return result;
        }

        public IEnumerable<SageEquipment> GetEquipments()
        {
            var userId = userService.Name;
            var equipments = unitOfWork.GetEntities<SageEquipment>().Get();
            var result = equipments.Where(x => x.Employee == userId);
            return result;
        }
        public SageTechnicianLocation SaveTechnicianLocation(string technicianId, decimal lat, decimal lng)
        {
             var techLocation = new SageTechnicianLocation
            {
                Employee = technicianId,
                Latitude = lat,
                Longitude = lng,
                Date = DateTime.Now
            };
            unitOfWork.TechnicianLocation.Add(techLocation);
            return techLocation;
        }
    }
}