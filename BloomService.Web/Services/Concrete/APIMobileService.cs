using BloomService.Domain.Entities;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Services.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BloomService.Web.Services.Concrete
{
    public class APIMobileService : IAPIMobileService
    {
        IWorkOrderSageApiService _workOrderSageApiService;
        IUserService _userService;
        IEmployeeSageApiService _employeeSageApiService;
        IImageService _imageService;
        IUnitOfWork _unitOfWork;

        public APIMobileService(IWorkOrderSageApiService workOrderSageApiService, IUserService userService, IEmployeeSageApiService employeeSageApiService, IImageService imageService, IUnitOfWork unitOfWork)
        {
            _imageService = imageService;
            _workOrderSageApiService = workOrderSageApiService;
            _userService = userService;
            _employeeSageApiService = employeeSageApiService;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SageWorkOrder> GetWorkOreders()
        {
            var userId = _userService.GetId();
            var workOrders = _workOrderSageApiService.Get();
            return workOrders.Where(x=>x.Employee == userId);
        }

        public bool AddImage(IEnumerable<string> images, string idWorkOrder)
        {
            var pathToImage = System.Web.Hosting.HostingEnvironment.MapPath("/Images/");
            var workOrder = _workOrderSageApiService.Get(idWorkOrder);
            if (workOrder == null)
                return false;
            var imagesDB = _unitOfWork.GetEntities<ImageWorkOrder>().GetById(idWorkOrder);
            var countImage = 0;
            if (imagesDB != null && imagesDB.Images!=null)
            {
                countImage = imagesDB.Images.Count();
            }
            else
            {
                imagesDB = new ImageWorkOrder();
                imagesDB.Images = new List<string>();
                imagesDB.WorkOrder = idWorkOrder;
            }
            foreach (var image in images)
            {
                var name = idWorkOrder.ToString() + countImage;
                var fileName = _imageService.SaveFile(image, pathToImage, name);
                imagesDB.Images.Add(fileName);
                countImage++;
            }
            _unitOfWork.GetEntities<ImageWorkOrder>().Update(imagesDB);
            return true;
        }
    }
}