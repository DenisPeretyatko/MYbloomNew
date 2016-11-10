using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Models;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Infrastructure;
using BloomService.Web.Infrastructure.Mongo;
using AutoMapper;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Web.Infrastructure.Queries;

namespace BloomService.Web.Controllers
{

    public class ApiMobileController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly ILog _log = LogManager.GetLogger(typeof(ApiMobileController));
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly IAuthorizationService _authorizationService;
        private readonly INotificationService _notification;
        private readonly IBloomServiceHub _hub;
        private readonly BloomServiceConfiguration _settings;

        public ApiMobileController(
            ISageApiProxy sageApiProxy,
            IImageService imageService,
            IRepository repository,
            IAuthorizationService authorizationService,
            INotificationService notification,
            IBloomServiceHub hub,
            BloomServiceConfiguration settings)
        {
            _sageApiProxy = sageApiProxy;
            _imageService = imageService;
            _repository = repository;
            _settings = settings;
            _authorizationService = authorizationService;
            _notification = notification;
            _hub = hub;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Apimobile/Authorization")]
        public ActionResult Authorize(string name, string password, string deviceToken)
        {
            var token = _authorizationService.Authorization(name, password);
            if (token == null)
            {
                return Error("Invalid login or password", "token == null, Authorization failed");
            }

            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == token.Id).FirstOrDefault();
            if (employee != null && !string.IsNullOrEmpty(deviceToken))
            {
                employee.IosDeviceToken = deviceToken;
                _repository.Update(employee);
            }
            return Json(new { access_token = token.Token }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Equipment/{part}")]
        public ActionResult GetEquipment(string part)
        {
            var result = _repository.SearchFor<SageEquipment>(x => x.Part == part);
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder/{id}")]
        public ActionResult GetWorkorder(long id)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id).Single();
            workOrder.Comments = workOrder.Comments.DecodeSafeHtmlString();
            workOrder.Images = workOrder.Images.Where(x => !x.IsDeleted).ToList();
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Part")]
        public ActionResult GetPart()
        {
            var result = _repository.GetAll<SagePart>().Avaliable();
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Repair")]
        public ActionResult GetRepair()
        {
            var result = _repository.GetAll<SageRepair>();
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder")]
        public ActionResult GetWorkorders()
        {
            var userId = UserModel.Name;

            var assignments = _repository.SearchFor<SageAssignment>().ToEmployee(userId).ToList();
            var allWorkorders = _repository.SearchFor<SageWorkOrder>().VisibleForTechnicain().ToList();

            var workorders = new List<SageWorkOrder>();

            foreach (var assignment in assignments)
            {
                var workorder = allWorkorders.SingleOrDefault(x => x.WorkOrder == assignment.WorkOrder);
                if (workorder != null)
                {
                    workorder.Images = workorder.Images.Where(x=>!x.IsDeleted).OrderBy(x => x.Id).ToList();
                    workorder.ScheduleDate = assignment.Start.TryAsDateTime();
                    workorders.Add(workorder);
                }
            }

            var locations = _repository.GetAll<SageLocation>();
            foreach (var order in workorders)
            {
                order.Equipments = new List<SageEquipment>();
                order.Images = _imageService.GetPhotoForWorkOrder(order.WorkOrder, _settings.SiteUrl);
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location != null)
                {
                    order.Latitude = location.Latitude;
                    order.Longitude = location.Longitude;
                    order.Address = string.Join(" ", string.Join(", ", new[] {location.Name, location.Address,location.City,location.State, location.ZIP }.Where(str => !string.IsNullOrEmpty(str))));
                }
                if (order.Equipment != 0)
                {
                    var equipments = _repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment).ToList();
                    foreach (var equipment in equipments)
                    {
                        if (equipment.InstallLocation == order.Location)
                            equipment.EquipmentType = string.Format($"{equipment.EquipmentType} - {equipment.InstallLocation}");
                    }
                    order.Equipments.AddRange(equipments);
                }
            }
            return Json(workorders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/AddItem")]
        public ActionResult AddWorkorderItem(LaborPartsModel model)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
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

                var result = _sageApiProxy.AddWorkOrderItem(workOrderItem);
                if (result != null && result.IsSucceed && result.Entity != null)
                {
                    var woiResult = _sageApiProxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                    if (woiResult.IsSucceed)
                    {
                        workOrder.WorkOrderItems = woiResult.Entities;
                        _repository.Update(workOrder);
                        _hub.UpdateSageWorkOrder(workOrder);
                    }
                }
                else
                {
                    _log.ErrorFormat("Was not able to save workorderItem to sage. !result.IsSucceed");
                    return Error ("Work order item save failed", $"AddWorkOrderItem method IsSucceed==false. {result?.ErrorMessage}"
                    );
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var resultUpdate = _sageApiProxy.EditWorkOrderItem(workOrderItem);
                if (resultUpdate != null && resultUpdate.IsSucceed && resultUpdate.Entity != null)
                {
                    var woiResult = _sageApiProxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                    if (woiResult.IsSucceed)
                    {
                        workOrder.WorkOrderItems = woiResult.Entities;
                        _repository.Update(workOrder);
                    }
                }
                else
                {
                    _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                    return Error("Work order item update failed", $"EditWorkOrderItem method IsSucceed==false. {resultUpdate?.ErrorMessage}."
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
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (workOrder == null)
                return Error("WorkOrder doesn't exists", $"There is no workorders with id {model.WorkOrder}. workOrder==null" );

            var item = workOrder.WorkOrderItems.SingleOrDefault(x => x.WorkOrderItem == workOrderItemId);
            var dBworkOrderItems = workOrder.WorkOrderItems.ToList();

            var result = _sageApiProxy.DeleteWorkOrderItems(model.WorkOrder, new List<long> { model.WorkOrderItem});
            if (result != null && result.IsSucceed)
            {
                dBworkOrderItems.Remove(item);
                workOrder.WorkOrderItems = dBworkOrderItems;
                _repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to update workorderItem to sage. !result.IsSucceed");
                return Error("Was not able to update workorderItem to sage", 
                    $"DeleteWorkOrderItems method IsSucceed==false. {(result != null ? result.ErrorMessage : string.Empty)}");
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
            var imageItem = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.WorkOrder).FirstOrDefault();
            if (imageItem == null)
            {
                _log.InfoFormat("Workorder {0} images not found", model.WorkOrder);
                return Error("Workorder images not found", $"There is no SageImageWorkOrder with Workorder {model.WorkOrder}. imageItem == null" );
            }
            var imageId = model.Id.AsInt();
            var image = imageItem.Images.FirstOrDefault(x => x.Id == imageId);
            if (image != null)
                image.IsDeleted = true;
            _repository.Update(imageItem);
            _hub.UpdateWorkOrderPicture(imageItem);
            _log.InfoFormat("Image ({0}) deleted. Repository updated", model.Id);
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
                Date = DateTime.Now.GetLocalDate(_settings.Timezone)
            };
            _repository.Add(techLocation);
            var emploee = _repository.SearchFor<SageEmployee>(x => x.Employee == userId).Single();
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
            var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.Id).FirstOrDefault();
            if (workorder == null)
                return Error("Workorder not found", $"There is no workorders with id {model.Id}. workorder == null" );

            var sageStatus = model.Status == WorkOrderStatus.Closed ?
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Closed) :
                                             WorkOrderStatus.ByStatus(WorkOrderStatus.Open);
            var result = _sageApiProxy.EditWorkOrderStatus(model.Id, sageStatus.ToString());
            if (!result.IsSucceed)
                return Error("Was not able to save workorder to sage",
                    $"EditWorkOrderStatus method IsSucceed==false. {result.ErrorMessage}." );

            workorder.Status = model.Status;
            _repository.Update(workorder);
            _hub.UpdateSageWorkOrder(workorder);
            _log.InfoFormat("Workorder ({0}) status changed. Status: {1}. Repository updated", workorder.Name, model.Status);
            _notification.SendNotification($"Workorder {workorder.Name} change status by {model.Status}");
            if (model.Status == WorkOrderStatus.WorkComplete)
            {
                var now = $"{DateTime.Now.GetLocalDate(_settings.Timezone):MM/dd/yyyy HH:mm tt}";
                _hub.ShowAlert(new SweetAlertModel()
                {
                    Title = "Work Order #<a ui-sref=\"manager.workorder.edit({ id:'" + workorder.Id + "'})\" class=\"close-sweet-alert\" href=\"/#/manager/workorders/" + workorder.Id + "/edit\">" + workorder.WorkOrder + "</a> Marked Complete",
                    Message = $"By {UserModel.Name} at {now}",
                    Type = "success"
                });
            }
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/AddNote")]
        public ActionResult AddNote(WorkOrderNoteModel model)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);
            var addNoteResult = _sageApiProxy.AddNote(note);
            var getNotesResult = _sageApiProxy.GetNotes(note.TRANSNBR);

            if (addNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                _repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
                _hub.AddNote(model);
            }
            else
            {
                _log.ErrorFormat("Was not able to add note to sage. !result.IsSucceed");
                return Error("Note save failed", $"AddNote method IsSucceed==false. {addNoteResult?.ErrorMessage}");
            }
            return Json(addNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/EditNote")]
        public ActionResult EditNote(WorkOrderNoteModel model)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);
            var editNoteResult = _sageApiProxy.EditNote(note);
            var getNotesResult = _sageApiProxy.GetNotes(note.TRANSNBR);

            if (editNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                _repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to save note to sage. !result.IsSucceed");
                return Error("Note save failed", $"EditNote method IsSucceed==false. {editNoteResult?.ErrorMessage}");
            }
            return Json(editNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/DeleteNote")]
        public ActionResult DeleteNote(WorkOrderNoteModel model)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");
            }

            var note = Mapper.Map<SageNote>(model);

            var deleteNoteResult = _sageApiProxy.DeleteNote(note.NOTENBR);
            var getNotesResult = _sageApiProxy.GetNotes(note.TRANSNBR);

            if (deleteNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                _repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to remove note from sage. !result.IsSucceed");
                return Error("Note delete failed", $"DeleteNote method IsSucceed==false. {deleteNoteResult?.ErrorMessage}");
            }
            return Json(deleteNoteResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/ChangeImageLocation")]
        public ActionResult ChangeImageLocation(ImageLocationModel model)
        {
            if(!_imageService.ChangeImageLocation(model))
                return Error("Images does not exist", $"There are no images with workorderID: {model.WorkOrderId}. images == null");
            return Success();
        }
    }
}