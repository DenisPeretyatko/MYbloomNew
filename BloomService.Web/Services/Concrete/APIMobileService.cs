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
    public class ApiMobileService : IApiMobileService
    {
        private readonly IEmployeeService employeeService;

        private readonly IImageService imageService;

        private readonly IUnitOfWork unitOfWork;

        private readonly IUserService userService;

        private readonly IWorkOrderService workOrderService;

        public ApiMobileService(
            IWorkOrderService workOrderService,
            IUserService userService,
            IEmployeeService employeeService,
            IImageService imageService,
            IUnitOfWork unitOfWork)
        {
            this.imageService = imageService;
            this.workOrderService = workOrderService;
            this.userService = userService;
            this.employeeService = employeeService;
            this.unitOfWork = unitOfWork;
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
            unitOfWork.GetEntities<SageImageWorkOrder>().Update(imagesDB);
            return true;
        }

        public IEnumerable<SageWorkOrder> GetWorkOreders()
        {
            var userId = userService.GetId();
            var workOrders = workOrderService.Get();
            var result = workOrders.Where(x => x.Employee == userId);
            var locations = unitOfWork.Locations.Get();
            foreach (var order in result)
            {
                var images = unitOfWork.GetEntities<SageImageWorkOrder>().SearchFor(x => x.WorkOrder == order.WorkOrder).FirstOrDefault();
                if (images != null)
                {
                    foreach(var image in images.Images)
                    {
                        image.Image = ConfigurationManager.AppSettings["BSUrl"] + "/Images/" + image.Image;
                    }
                    order.Images = images.Images;
                }
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location == null)
                    continue;
                order.Latitude = location.Latitude;
                order.Longitude = location.Longitude;
            }
            return result;
        }
    }
}