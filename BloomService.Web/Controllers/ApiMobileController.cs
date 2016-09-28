using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;
using MongoDB.Driver;

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

        [HttpPost]
        [AllowAnonymous]
        [Route("Apimobile/Authorization")]
        public ActionResult Get(string name, string password, string deviceToken)
        {
            var token = authorizationService.Authorization(name, password);
            if (token == null)
            {
                return Error("Invalid login or password", "token == null, Authorization failed");
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
        public ActionResult GetWorkOrder(long id)
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
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location != null)
                {
                    order.Latitude = location.Latitude;
                    order.Longitude = location.Longitude;
                    order.Address = string.Join(" ", String.Join(", ", new[] {location.Address,location.City,location.ZIP,location.State}.Where(str => !string.IsNullOrEmpty(str))));
                }                
                if (order.Equipment != 0)
                {
                    var equipments = repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment);
                    order.Equipments.AddRange(equipments);
                }
            }
            return Json(workorders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/AddItem")]
        public ActionResult AddWOItem(LaborPartsModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrder}. workOrder == null" );
            }

            var workOrderItem = Mapper.Map<SageWorkOrderItem>(model);
            workOrderItem.Employee = UserModel.Name;

            if (model.WorkOrderItem == 0)
            {
                workOrderItem.WorkOrder = model.WorkOrder;
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
                    var woiResult = sageApiProxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                    if (woiResult.IsSucceed)
                    {
                        workOrder.WorkOrderItems = woiResult.Entities;
                        repository.Update(workOrder);
                        _hub.UpdateSageWorkOrder(workOrder);
                    }
                }
                else
                {
                    _log.ErrorFormat("Was not able to save workorderItem to sage. !result.IsSucceed");
                    return Error ("Work order item save failed", $"AddWorkOrderItem method IsSucceed==false. {result?.ErrorMassage}"
                    );
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var resultUpdate = sageApiProxy.EditWorkOrderItem(workOrderItem);
                if (resultUpdate != null && resultUpdate.IsSucceed && resultUpdate.Entity != null)
                {
                    var woiResult = sageApiProxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                    if (woiResult.IsSucceed)
                    {
                        workOrder.WorkOrderItems = woiResult.Entities;
                        repository.Update(workOrder);
                    }
                }
                else
                {
                    _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                    return Error("Work order item update failed", $"EditWorkOrderItem method IsSucceed==false. {resultUpdate?.ErrorMassage}."
                    );
                }
                return Json(resultUpdate, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("Apimobile/DeleteItem")]
        public ActionResult DeleteWorkOrderItem(LaborPartsModel model)
        {
            var workOrderItemId = model.WorkOrderItem;
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (workOrder == null)
                return Error("WorkOrder doesn't exists", $"There is no workorders with id {model.WorkOrder}. workOrder==null" );

            var item = workOrder.WorkOrderItems.SingleOrDefault(x => x.WorkOrderItem == workOrderItemId);
            var dBworkOrderItems = workOrder.WorkOrderItems.ToList();

            var result = sageApiProxy.DeleteWorkOrderItems(model.WorkOrder, new List<long> { model.WorkOrderItem});
            if (result != null && result.IsSucceed)
            {
                dBworkOrderItems.Remove(item);
                workOrder.WorkOrderItems = dBworkOrderItems;
                repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                return Error("Was not able to update workorderItem to sage", 
                    $"DeleteWorkOrderItems method IsSucceed==false. {(result != null ? result.ErrorMassage : string.Empty)}");
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
                _log.InfoFormat("Add image failed");
                return Error("Add image failed", "SavePhotoForWorkOrder method return null. Add image failed." );
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
                return Error("Workorder images not found", $"There is no SageImageWorkOrder with Workorder {model.WorkOrder}. imageItem == null" );
            }
            var imageId = model.Id.AsInt();
            var image = imageItem.Images.FirstOrDefault(x => x.Id == imageId);
            imageItem.Images.Remove(image);
            repository.Update(imageItem);
            _hub.UpdateWorkOrderPicture(imageItem);
            _log.InfoFormat("Image ({0}) deleted. Repository updated", model.Id);
          //  notification.SendNotification(string.Format("Image {0} deleted. Repository updated", model.Id));
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/CommentImage")]
        public ActionResult ComentWorkOrderImage(CommentImageModel model)
        {
            if (!_imageService.SaveDescriptionsForPhoto(model))
            {
                _log.InfoFormat("Add description to image failed");
                return Error("Add description to image failed", $"There is no SageImageWorkOrder for Workorder, or SageImageWorkOrder item dont dontain Image. Add description to image failed" );
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
                Employee = Convert.ToInt64(userId),
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

        //[HttpPost]
        //[Route("Apimobile/ChangeWorkOrderNotes")]
        //public ActionResult ChangeWorkOrderNotes(NotesModel model)
        //{
        //    _log.InfoFormat("Method: AddNotesToWorkOrder. Workorder Id: {0}", model.WorkOrderId);

        //    var workorder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).FirstOrDefault();
        //    if (workorder == null)
        //        return Error("Workorder not found", $"There is no workorders with id {model.WorkOrderId}. workorder == null");

        //    var result = sageApiProxy.EditWorkOrder(new SageWorkOrder()
        //    {
        //        WorkOrder = model.WorkOrderId,
        //        Comments = model.Notes
        //    });

        //    if (result == null)
        //    {
        //        _log.InfoFormat("Work order notes changing failed");
        //        return Error("Work order notes changing failed", "EditWorkOrder method return null. Work order notes changing failed.");
        //    }

        //    if (result.Entity != null)
        //    {
        //        result.Entity.Id = workorder.Id;
        //        repository.Update(result.Entity);
        //        _hub.UpdateSageWorkOrder(result.Entity);
        //    }

        //    _log.InfoFormat("Work order notes change success");
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Route("Apimobile/ChangeWorkorderStatus")]
        public ActionResult ChangeWorkorderStatus(StatusModel model)
        {
            _log.InfoFormat("Method: ChangeWorkorderStatus. Id: {0}, Status {1}", model.Id, model.Status);
            var workorder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.Id).FirstOrDefault();
            if (workorder == null)
                return Error("Workorder not found", $"There is no workorders with id {model.Id}. workorder == null" );

            var sageStatus = model.Status == WorkOrderStatus.Closed ?
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Closed) :
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Open);
            var result = sageApiProxy.EditWorkOrderStatus(model.Id, sageStatus.ToString());
            if (!result.IsSucceed)
                return Error("Was not able to save workorder to sage",
                    $"EditWorkOrderStatus method IsSucceed==false. {result.ErrorMassage}." );

            workorder.Status = model.Status;
            repository.Update(workorder);
            _hub.UpdateSageWorkOrder(workorder);
            _log.InfoFormat("Workorder ({0}) status changed. Status: {1}. Repository updated", workorder.Name, model.Status);
            notification.SendNotification($"Workorder {workorder.Name} change status by {model.Status}");
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/AddNote")]
        public ActionResult AddNote(WorkOrderNoteModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);
            var addNoteResult = sageApiProxy.AddNote(note);
            var getNotesResult = sageApiProxy.GetNotes(note.TRANSNBR);

            if (addNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to add note to sage. !result.IsSucceed");
                return Error("Note save failed", $"AddNote method IsSucceed==false. {addNoteResult?.ErrorMassage}"
                );
            }
            return Json(addNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/EditNote")]
        public ActionResult EditNote(WorkOrderNoteModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);
            var editNoteResult = sageApiProxy.EditNote(note);
            var getNotesResult = sageApiProxy.GetNotes(note.TRANSNBR);

            if (editNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to save note to sage. !result.IsSucceed");
                return Error("Note save failed", $"EditNote method IsSucceed==false. {editNoteResult?.ErrorMassage}"
                );
            }
            return Json(editNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/DeleteNote")]
        public ActionResult DeleteNote(WorkOrderNoteModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);

            var deleteNoteResult = sageApiProxy.DeleteNote(note.NOTENBR);
            var getNotesResult = sageApiProxy.GetNotes(note.TRANSNBR);

            if (deleteNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to remove note from sage. !result.IsSucceed");
                return Error("Note delete failed", $"DeleteNote method IsSucceed==false. {deleteNoteResult?.ErrorMassage}"
                );
            }
            return Json(deleteNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/ChangeImageLocation")]
        public ActionResult ChangeImageLocation(ImageLocationModel model)
        {
            if(!_imageService.ChangeImageLocation(model) )
            return Error("Images does not exist",
                    $"There are no images with workorderID: {model.WorkOrderId}. images == null");
            return Success();
        }
    }
}