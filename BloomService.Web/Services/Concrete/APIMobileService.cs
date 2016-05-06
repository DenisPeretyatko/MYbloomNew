namespace BloomService.Web.Services.Concrete
{
using System.Collections.Generic;
using System.Linq;
    using System.Web.Hosting;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;

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

        public bool AddImage(IEnumerable<string> images, string idWorkOrder)
        {
            var pathToImage = HostingEnvironment.MapPath("/Images/");
            var workOrder = workOrderService.Get(idWorkOrder);
            if (workOrder == null)
            {
                return false;
            }

            var imagesDB = unitOfWork.GetEntities<SageImageWorkOrder>().Get(idWorkOrder);
            var countImage = 0;
            if (imagesDB != null && imagesDB.Images != null)
            {
                countImage = imagesDB.Images.Count();
            }
            else
            {
                imagesDB = new SageImageWorkOrder { Images = new List<string>(), WorkOrder = idWorkOrder };
            }

            foreach (var image in images)
            {
                var name = idWorkOrder + countImage;
                var fileName = imageService.SaveFile(image, pathToImage, name);
                imagesDB.Images.Add(fileName);
                countImage++;
            }

            unitOfWork.GetEntities<SageImageWorkOrder>().Update(imagesDB);
            return true;
        }

        public IEnumerable<SageWorkOrder> GetWorkOreders()
        {
            var userId = userService.GetId();
            var workOrders = workOrderService.Get();
            return workOrders.Where(x => x.Employee == userId);
        }
    }
}