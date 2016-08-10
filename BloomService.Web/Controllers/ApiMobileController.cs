using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Domain.Entities.Concrete;
    using Domain.Extensions;
    using Models;
    using Infrastructure.SignalR;
    using Infrastructure;
    using Infrastructure.Mongo;
    using AutoMapper;
    using Infrastructure.Constants;
    using Infrastructure.Queries;
    public class ApiMobileController : BaseController
    {
        private readonly IImageService _imageService;

        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));

        private readonly IRepository repository;

        private readonly ISageApiProxy sageApiProxy;

        private readonly IAuthorizationService authorizationService;

        private readonly INotificationService notification;

        private readonly BloomServiceConfiguration settings;

        private readonly IBloomServiceHub _hub;

        public ApiMobileController(
            ISageApiProxy sageApiProxy,
            IImageService imageService,
            IRepository repository,
            IAuthorizationService authorizationService,
            INotificationService notification,
            BloomServiceConfiguration settings,
            IBloomServiceHub hub)
        {
            this.sageApiProxy = sageApiProxy;
            this._imageService = imageService;
            this.repository = repository;
            this.settings = settings;
            this.authorizationService = authorizationService;
            this.notification = notification;
            _hub = hub;
        }

        //[HttpPost]
        //[Route("Apimobile/Workorder/{id}/Equipment")]
        //public ActionResult AddEquipmentToWorkOrder()
        //{
        //    _log.Info("Method: AddEquipmentToWorkOrder");
        //    return Success();
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("Apimobile/Authorization")]
        public ActionResult Get(string name, string password, string deviceToken)
        {
            var token = authorizationService.Authorization(name, password);
            if (token == null)
            {
                return Error("Invalid login or password");
            }
            var employee = repository.SearchFor<SageEmployee>(x => x.Employee == token.Id).FirstOrDefault();
            if (employee != null && !string.IsNullOrEmpty(deviceToken))
            {
                employee.IosDeviceToken = deviceToken;
                repository.Update(employee);
            }
            return Json(new { access_token = token.Token }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Equipment/{part}")]
        public ActionResult GetEquipment(string part)
        {
            var result = repository.SearchFor<SageEquipment>(x => x.Part == part);
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder/{id}")]
        public ActionResult GetWorkOrder(string id)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Part")]
        public ActionResult GetPart()
        {
            var result = repository.GetAll<SagePart>().Avaliable();
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Repair")]
        public ActionResult GetRepair()
        {
            var result = repository.GetAll<SageRepair>();
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder")]
        public ActionResult GetWorkOrders()
        {
            var userId = UserModel.Name;

            var assignments = repository.SearchFor<SageAssignment>().ToEmployee(userId).ToList();
            var allWorkorders = repository.SearchFor<SageWorkOrder>().VisibleForTechnicain().ToList();

            var workorders = new List<SageWorkOrder>();

            foreach (var assignment in assignments)
            {
                var workorder = allWorkorders.SingleOrDefault(x => x.WorkOrder == assignment.WorkOrder);
                if (workorder != null)
                {
                    workorder.Images = workorder.Images.OrderBy(x => x.Id).ToList();
                    workorder.ScheduleDate = assignment.Start.TryAsDateTime();
                    workorders.Add(workorder);
                }
            }

            var locations = repository.GetAll<SageLocation>();
            foreach (var order in workorders)
            {
                order.Equipments = new List<SageEquipment>();
                order.Images = _imageService.GetPhotoForWorkOrder(order.WorkOrder, settings.SiteUrl);
                if (order.Images != null)
                {
                    order.Images.OrderBy(x => x.Id).ToList();
                }

                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location != null)
                {
                    order.Latitude = location.Latitude;
                    order.Longitude = location.Longitude;
                    order.Address = location.FullAddress;
                    if (order.Equipment != 0)
                    {
                        var equipments = repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment.ToString());
                        order.Equipments.AddRange(equipments);
                    }
                }
            }
            return Json(workorders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/AddItem")]
        public ActionResult AddWorkOrderItem(LaborPartsModel model)
        {
            var workOrderItemId = model.WorkOrderItem.AsInt();
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();

            var workOrderItem = Mapper.Map<SageWorkOrderItem>(model);
            var dBworkOrderItems = new List<SageWorkOrderItem>();

            if (workOrder.WorkOrderItems == null || (workOrder.WorkOrderItems != null && workOrder.WorkOrderItems.SingleOrDefault(x => x.WorkOrderItem == workOrderItemId) == null))
            {
                if (workOrder.WorkOrderItems != null)
                {
                    dBworkOrderItems = workOrder.WorkOrderItems.ToList();
                }
                workOrderItem.WorkOrder = model.WorkOrder.AsInt();
                workOrderItem.TotalSale = workOrderItem.Quantity * workOrderItem.UnitSale;
                if (workOrderItem.ItemType == "Parts")
                {
                    workOrderItem.PartsSale = workOrderItem.UnitSale;
                }
                else
                {
                    workOrderItem.LaborSale = workOrderItem.UnitSale;
                }
                
                var result = sageApiProxy.AddWorkOrderItem(workOrderItem);
                if (result != null && result.IsSucceed && result.Entity != null)
                {
                    result.Entity.Part = model.Part;
                    dBworkOrderItems.Add(result.Entity);
                    workOrder.WorkOrderItems = dBworkOrderItems;
                    repository.Update(workOrder);
                }
                else
                {
                    _log.ErrorFormat("Was not able to save workorderItem to sage. !result.IsSucceed");
                    return Error("Was not able to save workorderItem to sage");
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            dBworkOrderItems = workOrder.WorkOrderItems.ToList();
            workOrderItem.WorkOrder = model.WorkOrder.AsInt();
            var resultUpdate = sageApiProxy.EditWorkOrderItem(workOrderItem);
            if (resultUpdate != null && resultUpdate.IsSucceed && resultUpdate.Entity != null)
            {
                var dbEntity = dBworkOrderItems.SingleOrDefault(x => x.WorkOrderItem == workOrderItemId);
                dBworkOrderItems.Remove(dbEntity);
                resultUpdate.Entity.Part = model.Part;
                dBworkOrderItems.Add(resultUpdate.Entity);
                workOrder.WorkOrderItems = dBworkOrderItems;
                repository.Update(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                return Error("Was not able to update workorderItem to sage");
            }
            return Json(resultUpdate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/DeleteItem")]
        public ActionResult DeleteWorkOrderItem(LaborPartsModel model)
        {
            var workOrderItemId = model.WorkOrderItem.AsInt();
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (workOrder == null)
                return Error("WorkOrder doesn't exists");

            var item = workOrder.WorkOrderItems.SingleOrDefault(x => x.WorkOrderItem == workOrderItemId);
            var dBworkOrderItems = workOrder.WorkOrderItems.ToList();

            var result = sageApiProxy.DeleteWorkOrderItems(model.WorkOrder.AsInt(), new List<int> { model.WorkOrderItem.AsInt()});
            if (result != null && result.IsSucceed)
            {
                dBworkOrderItems.Remove(item);
                workOrder.WorkOrderItems = dBworkOrderItems;
                repository.Update(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                return Error("Was not able to update workorderItem to sage");
            }
            return Json(Success(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/Image")]
        public ActionResult PostWorkOrderImage(ImageModel model)
        {
            _log.InfoFormat("Method: PostImage. Workorder Id: {0}", model.IdWorkOrder);
            var result = _imageService.SavePhotoForWorkOrder(model);
            if (result == null)
            {
                _log.InfoFormat("Add image faild");
                return Error("Add image faild");
            }

            _log.InfoFormat("Add image for workorder success");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/DeletePicture")]
        public ActionResult RemoveWorkOrderPicture(PictureModel model)
        {
            _log.InfoFormat("Method: DeletePicture. Id: {0}, WorkOrder {1}", model.Id, model.WorkOrder);
            var imageItem = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.WorkOrder).FirstOrDefault();
            if (imageItem == null)
            {
                _log.InfoFormat("Workorder {0} images not found", model.WorkOrder);
                return Error("Workorder images not found");
            }
            var imageId = model.Id.AsInt();
            var image = imageItem.Images.FirstOrDefault(x => x.Id == imageId);
            imageItem.Images.Remove(image);
            repository.Update(imageItem);
            _log.InfoFormat("Image ({0}) deleted. Repository updated", model.Id);
            notification.SendNotification(string.Format("Image {0} deleted. Repository updated", model.Id));
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/CommentImage")]
        public ActionResult ComentWorkOrderImage(CommentImageModel model)
        {
            if (!_imageService.SaveDescriptionsForPhoto(model))
            {
                _log.InfoFormat("Add descriptions image failed");
                return Error("Add descriptions image faild");
            }

            _log.InfoFormat("Add image for workorder success");
            return Success();               
        }

        [HttpPost]
        [Route("Apimobile/Location")]
        public ActionResult UpdateTechnicianLocation(decimal lat, decimal lng)
        {
            var userId = UserModel.Id;
            _log.InfoFormat("Method: PostLocation. technicianId: {0}, lat: {1}, lng {2}", userId, lat, lng);
            
            var techLocation = new SageTechnicianLocation
            {
                Employee = userId,
                Latitude = lat,
                Longitude = lng,
                Date = DateTime.Now.GetLocalDate()
            };
            repository.Add(techLocation);
            var emploee = repository.SearchFor<SageEmployee>(x => x.Employee == userId).Single();
            emploee.Longitude = lat;
            emploee.Latitude = lng;
            _hub.UpdateTechnicianLocation(emploee);
            _log.InfoFormat("TechLocation added. TechnicianId: {0}", userId);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/ChangeWorkorderStatus")]
        public ActionResult ChangeWorkorderStatus(StatusModel model)
        {
            _log.InfoFormat("Method: ChangeWorkorderStatus. Id: {0}, Status {1}", model.Id, model.Status);
            var workorder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.Id).FirstOrDefault();
            if (workorder == null)
                return Error("Workorder not found");

            var sageStatus = model.Status == WorkOrderStatus.Closed ?
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Closed) :
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Open);
            var result = sageApiProxy.EditWorkOrderStatus(model.Id, sageStatus.ToString());
            if (!result.IsSucceed)
                return Error("Was not able to save workorder to sage");

            workorder.Status = model.Status;
            repository.Update(workorder);
            _log.InfoFormat("Workorder ({0}) status changed. Status: {1}. Repository updated", workorder.Name, model.Status);
            notification.SendNotification(string.Format("Workorder {0} change status by {1}", workorder.Name, model.Status));
            return Success();
        }
    }
}