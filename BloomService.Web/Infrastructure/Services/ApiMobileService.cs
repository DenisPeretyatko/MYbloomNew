using System;

namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;

    using Domain.Entities.Concrete;
    using Abstract;
    using Models.Request;
    using Domain.Extensions;
    using Domain.Repositories.Abstract;

    public class ApiMobileService : IApiMobileService
    {

        private readonly IImageService imageService;

        private readonly IUserService userService;

        private readonly BloomServiceConfiguration _settings;

        private readonly IRepository _repository;

        public ApiMobileService(
            IUserService userService,
            IImageService imageService,
            BloomServiceConfiguration bloomConfiguration, IRepository repository)
        {
            this.imageService = imageService;
            this.userService = userService;
            _settings = bloomConfiguration;
            _repository = repository;
        }

        public bool AddImage(ImageModel model)
        {
            var pathToImage = HostingEnvironment.MapPath("/Images/");
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return false;
            }

            var imagesDB = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            var countImage = 0;
            if (imagesDB != null && imagesDB.Images != null)
            {
                countImage = imagesDB.Images.Count();
            }
            else
            {
                imagesDB = new SageImageWorkOrder { Images = new List<ImageLocation>(), WorkOrder = model.IdWorkOrder, WorkOrderBsonId = workOrder.Id};
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
            _repository.Add(imagesDB);
            return true;
        }

        public IEnumerable<SageWorkOrder> GetWorkOreders()
        {
            var userId = "Rinta, Chriss";

            var workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
            var result = workOrders.Where(x => x.Employee == userId);
            var locations = _repository.GetAll<SageLocation>();
            foreach (var order in result)
            {
                order.Equipments = new List<SageEquipment>();
                var images = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == order.WorkOrder).SingleOrDefault();
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
                    var equipments = _repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment.ToString());
                    order.Equipments.AddRange(equipments);
                }
            }
            return result;
        }

        public IEnumerable<SageEquipment> GetEquipments()
        {
            var userId = "Rinta, Chriss";
            var result = _repository.SearchFor<SageEquipment>(x => x.Employee == userId);
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
            _repository.Add(techLocation);
            return techLocation;
        }
    }
}