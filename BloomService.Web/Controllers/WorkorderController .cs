using Common.Logging;
using System.Web.Mvc;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Controllers
{
   public class WorkorderController : BaseController
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(WorkorderController));
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly IBloomServiceHub _hub;
        private readonly INotificationService _notification;
        private readonly IScheduleService _scheduleService;
        private readonly IImageService _imageService;
        private readonly BloomServiceConfiguration _settings;

        public WorkorderController(IRepository repository,
            ISageApiProxy sageApiProxy,
            IBloomServiceHub hub,
            INotificationService notification,
            IScheduleService scheduleService,
            IImageService imageService,
            BloomServiceConfiguration settings)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _notification = notification;
            _hub = hub;
            _scheduleService = scheduleService;
            _imageService = imageService;
            _settings = settings;
        }

        [HttpPost]
        [Route("Workorder/Create")]
        public ActionResult CreateWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: CreateWorkOrder. Model ID {0}", model.Id);
            if (string.IsNullOrEmpty(model.Calltype))
                return Error("Call Type is required");

            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.GetLocalDate(_settings.Timezone),
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee.ToString(),
                EstimatedRepairHours = model.Estimatehours.AsDecimal(),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods,
                JCJob = model.JCJob,
                Contact = model.Contact,
                Equipment = model.Equipment,
                DateClosed = new DateTime(2000, 1, 1).Date
            };

            var result = _sageApiProxy.AddWorkOrder(workorder);
            result.Entity.LocationNumber = long.Parse(model.Location);

            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to save workorder to sage. !result.IsSucceed");
                return Error(result.ErrorMessage);
            }

            var assignment = result.RelatedAssignment;

            assignment.IsValid = true;
            if (model.Emploee == 0)
            {
                result.Entity.AssignmentId = assignment.Assignment;
                _repository.Add(assignment);
            }
            else
            {
                var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Emploee).SingleOrDefault();
                if (employee != null && !employee.IsAvailable)
                {
                    result.Entity.AssignmentId = assignment.Assignment;
                    _repository.Add(assignment);
                }
                else
                {
                    var assignmentStart = (model.AssignmentDate.Date).Add((model.AssignmentTime).TimeOfDay);
                    var assignmentEnd = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay)
                        .AddHours(assignment.EstimatedRepairHours.AsDouble() == 0
                            ? 1
                            : assignment.EstimatedRepairHours.AsDouble());
                    assignment.EmployeeId = employee?.Employee ?? 0;
                    assignment.Start = assignmentStart.ToString();
                    assignment.End = assignmentEnd.ToString();
                    if (!_scheduleService.HasСrossoverAssignment(employee.Name, assignmentStart, assignmentEnd, model.WorkOrder))
                    {
                        assignment.Color = employee?.Color ?? "";
                        assignment.Customer = result.Entity.ARCustomer;
                        assignment.Location = result.Entity.Location;

                        var locations = _repository.GetAll<SageLocation>().ToArray();
                        var itemLocation = locations.FirstOrDefault(l => l.Name == result.Entity.Location);
                        result.Entity.ScheduleDate = assignment.ScheduleDate;
                        result.Entity.Latitude = itemLocation.Latitude;
                        result.Entity.Longitude = itemLocation.Longitude;

                        _repository.Add(assignment);
                        _hub.CreateAssignment(new AssignmentHubModel
                        {
                            WorkOrder = result.Entity,
                            DateEntered = assignment.ScheduleDate,
                            Employee = employee?.Employee ?? 0,
                            Color = employee?.Color ?? "",
                            Start = assignment.Start,
                            End = assignment.End
                        });
                    }
                }
            }

            _repository.Add(result.Entity);
            _log.InfoFormat("Workorder added to repository. ID: {0}, Name: {1}", workorder.Id, workorder.Name);
            _notification.SendNotification($"{workorder.WorkOrder} was created");
            return Success();
        }

        [HttpGet]
        [Route("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workorder = _repository.SearchFor<SageWorkOrder>(x => x.Id == id).Single();
            workorder.CustomerObj = _repository.SearchFor<SageCustomer>(x => x.Customer == workorder.ARCustomer).SingleOrDefault();
            workorder.LocationObj = _repository.SearchFor<SageLocation>(x => x.Location == workorder.LocationNumber).SingleOrDefault();
            workorder.CalltypeObj = _repository.SearchFor<SageCallType>(x => x.Description == workorder.CallType).SingleOrDefault();
            workorder.ProblemObj = _repository.SearchFor<SageProblem>(x => x.Description == workorder.Problem).SingleOrDefault();
            workorder.RateSheetObj = _repository.SearchFor<SageRateSheet>().ToList().SingleOrDefault(x => x.DESCRIPTION.Trim() == workorder.RateSheet);
            workorder.EmployeeObj = _repository.SearchFor<SageEmployee>(x => x.Name == workorder.Employee).SingleOrDefault();
            workorder.HourObj = _repository.SearchFor<SageRepair>(x => x.Repair == workorder.EstimatedRepairHours.ToString()).SingleOrDefault();
            workorder.PermissionCodeObj = _repository.SearchFor<SagePermissionCode>().ToList().SingleOrDefault(x => x.DESCRIPTION.Trim() == workorder.PermissionCode);
            workorder.PaymentMethodObj = PaymentMethod.PaymentMethods.FirstOrDefault(x => x.Method == workorder.PayMethod.Trim());
            workorder.StatusObj = WorkOrderStatus.Status.Single(x => x.Status == workorder.Status);
            workorder.Comments = workorder.Comments.DecodeSafeHtmlString();
            return Success(workorder);
        }

        [HttpGet]
        [Route("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(long id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == id).SingleOrDefault();
            if (pictures == null)
                return Success("");

            pictures.Images = pictures.Images.Where(x => !x.IsDeleted)
                     .OrderBy(x => x.Id)
                     .ToList();

            return Success(pictures);
        }

        [HttpPost]
        [Route("EditComment")]
        public ActionResult EditWorkorderComment(EditCommentModel model)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (pictures == null)
                return Success("");

            pictures.Images.Find(x => x.Id == model.Id && !x.IsDeleted).Description = model.Comment;
            _repository.Update(pictures);
            _hub.UpdateWorkOrderPicture(pictures);
            return Success();
        }

        [HttpGet]
        [Route("Workorder")]
        public ActionResult GetWorkorders()
        {
            var workoders = _repository.SearchFor<SageWorkOrder>()
                .OrderByDescending(x => x.DateEntered)
                .Take(500).ToList();
            var workorderModels = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(workoders);
            return Success(workorderModels);
        }

        [HttpPost]
        [Route("Workorder/CustomerByLocation")]
        public ActionResult GetCustomerByLocation(string arcustomer)
        {
            if (string.IsNullOrEmpty(arcustomer))
            {
                var customers = _repository.GetAll<SageCustomer>();
                return Success(customers);
            }

            var customer = _repository.GetAll<SageCustomer>()
                .SingleOrDefault(x => x.Customer == arcustomer) ?? new SageCustomer();
            return Success(customer);
        }

        [HttpPost]
        [Route("Workorder/LocationsByCustomer")]
        public ActionResult GetLocationsByCustomer(string customer)
        {
            if (string.IsNullOrEmpty(customer))
                return Success(new List<SageLocation>());

            var locations = _repository.GetAll<SageLocation>().Where(x => x.ARCustomer == customer).ToArray();
            return Success(locations);
        }

        [HttpPost]
        [Route("Workorder/EquipmentByLocation")]
        public ActionResult GetEquipmentByLocation(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Success(new List<SageEquipment>());

            var equipments = _repository.GetAll<SageEquipment>().Where(x => x.Location == name).ToArray();
            foreach (var equipment in equipments)
            {
                if (equipment.InstallLocation != "")
                    equipment.EquipmentType = string.Format($"{equipment.EquipmentType} - {equipment.InstallLocation}");
            }
            return Success(equipments);
        }

        [HttpPost]
        [Route("WorkorderPage")]
        public ActionResult GetWorkorderPage(WorkorderSortModel model)
        {
            var search = model.Search.AsLongSafe(-1);
            var workorders = _repository.GetAll<SageWorkOrder>();

            if (!string.IsNullOrEmpty(model.Search))
            {
                workorders = _repository.GetAll<SageWorkOrder>()
                        .Where(x => x.ARCustomer.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Location.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Status.ToLower().Contains(model.Search.ToLower()) ||
                                    x.WorkOrder == search);
            }

            var entitiesCount = workorders.Count();

            switch (model.Column) //sort
            {
                case "num":
                    workorders = model.Direction ? workorders.OrderBy(x => x.WorkOrder) : workorders.OrderByDescending(x => x.WorkOrder);
                    break;
                case "date":
                    workorders = model.Direction ? workorders.OrderBy(x => x.ScheduleDate) : workorders.OrderByDescending(x => x.ScheduleDate);
                    break;
                case "entered date":
                    workorders = model.Direction ? workorders.OrderBy(x => x.DateEntered) : workorders.OrderByDescending(x => x.DateEntered);
                    break;
                case "customer":
                    workorders = model.Direction ? workorders.OrderBy(x => x.ARCustomer) : workorders.OrderByDescending(x => x.ARCustomer);
                    break;
                case "location":
                    workorders = model.Direction ? workorders.OrderBy(x => x.Location) : workorders.OrderByDescending(x => x.Location);
                    break;
                case "address":
                    workorders = model.Direction ? workorders.OrderBy(x => x.Address) : workorders.OrderByDescending(x => x.Address);
                    break;
                case "assignment":
                    workorders = model.Direction ? workorders.OrderBy(x => x.AssignmentId) : workorders.OrderByDescending(x => x.AssignmentId);
                    break;
                case "status":
                    workorders = model.Direction ? workorders.OrderBy(x => x.Status) : workorders.OrderByDescending(x => x.Status);
                    break;
                case null:
                    workorders = workorders.OrderByDescending(x => x.ScheduleDate);
                    break;
            }

            if (entitiesCount > _settings.ItemsOnPage)
                workorders = workorders.Skip(model.Index * _settings.ItemsOnPage).Take(_settings.ItemsOnPage);

            var workorderList = workorders.ToList();
            var tmpLocations = workorderList.Select(x => x.Location);
            var locations = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Name)).ToList();
            foreach (var obj in workorderList)
            {
                if (obj.TimeEntered == null) continue;

                // TODO Fix this convertation.
                obj.CallDate = obj.DateEntered?.Date.Add(((DateTime)obj.TimeEntered).TimeOfDay) ?? DateTime.MinValue;
                var location = locations.FirstOrDefault(x => x.Name == obj.Location);
                if (location != null)
                {
                    obj.Latitude = location.Latitude;
                    obj.Longitude = location.Longitude;
                    obj.Address = string.Join(" ", string.Join(", ", new[] { location.Name, location.Address, location.City, location.State, location.ZIP }.Where(str => !string.IsNullOrEmpty(str))));
                }
            }

            var result = new WorkorderSortViewModel()
            {
                CountPage = entitiesCount % _settings.ItemsOnPage == 0 ?
                    entitiesCount / _settings.ItemsOnPage :
                    entitiesCount / _settings.ItemsOnPage + 1,
                WorkordersList = workorderList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkorderPageCount")]
        public ActionResult GetWorkorderPageCount()
        {
            var entitiesCount = _repository.GetAll<SageWorkOrder>().Count(x => !string.IsNullOrEmpty(x.Employee));
            var countPage = entitiesCount % _settings.ItemsOnPage == 0 ?
                entitiesCount / _settings.ItemsOnPage :
                entitiesCount / _settings.ItemsOnPage + 1;
            return Json(countPage, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [Route("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: SaveWorkOrder. Model ID {0}", model.Id);
            if (model.Location == null)
                return Error("Location is required");
            if (model.Problem == null)
                return Error("Problem is required");

            ResponceModel createAssignmentResult = new ResponceModel { IsSucceed = true };
            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.GetLocalDate(_settings.Timezone),
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee.ToString(),
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods,
                WorkOrder = model.WorkOrder,
                Id = model.Id,
                Status = WorkOrderStatus.ById(model.Status),
                JCJob = model.JCJob,
                Contact = model.Contact,
                Equipment = model.Equipment
            };

            var workOrderResult = _sageApiProxy.EditWorkOrder(workorder);
            workOrderResult.Entity.LocationNumber = long.Parse(model.Location);
            if (!workOrderResult.IsSucceed)
            {
                _log.ErrorFormat("Was not able to update workorder to sage. !result.IsSucceed");
                return Error("Was not able to update workorder to sage");
            }

            if (model.Emploee != 0)
            {
                var assignmentDb = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
                var editedAssignment = new AssignmentViewModel();
                editedAssignment.Employee = model.Emploee;
                editedAssignment.EndDate = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay).AddHours(assignmentDb.EstimatedRepairHours.AsDouble() == 0 ? 1 : assignmentDb.EstimatedRepairHours.AsDouble());
                editedAssignment.EstimatedRepairHours = workorder.EstimatedRepairHours.ToString();
                editedAssignment.Id = assignmentDb.Id;
                editedAssignment.ScheduleDate = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay);
                editedAssignment.WorkOrder = assignmentDb.WorkOrder;
                createAssignmentResult = _scheduleService.CerateAssignment(editedAssignment);
                if (createAssignmentResult.IsSucceed)
                {
                    var locations = _repository.GetAll<SageLocation>().ToArray();
                    var itemLocation = locations.FirstOrDefault(l => l.Name == workOrderResult.Entity.Location);
                    workOrderResult.Entity.ScheduleDate = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay);
                    workOrderResult.Entity.Latitude = itemLocation.Latitude;
                    workOrderResult.Entity.Longitude = itemLocation.Longitude;
                }
            }

            ResolveWorkOrderItems(model);
            ResolveWorkOrderNotes(model);

            workOrderResult.Entity.Status = WorkOrderStatus.ById(model.Status);
            workOrderResult.Entity.Id = workorder.Id;
            workOrderResult.Entity.IsValid = true;
            workOrderResult.Entity.WorkOrderItems = _sageApiProxy.GetWorkorderItemsByWorkOrderId(workorder.WorkOrder).Entities;
            workOrderResult.Entity.WorkNotes = this._sageApiProxy.GetNotes(workorder.WorkOrder).Entities;
            
            if (model.Status == WorkOrderStatus.WorkCompleteId)
            {
                var now = $"{DateTime.Now.GetLocalDate(_settings.Timezone):MM/dd/yyyy HH:mm tt}";
                _hub.ShowAlert(new SweetAlertModel()
                {
                    Title = "Work Order #<a ui-sref=\"manager.workorder.edit({ id:'"+workorder.Id+ "'})\" class=\"close-sweet-alert\" href=\"/#/manager/workorders/" + workorder.Id + "/edit\">" + model.WorkOrder+"</a> Marked Complete",
                    Message = $"By {UserModel.Name} at {now}",
                    Type = "success"
                });
            }
            else if (model.Status == WorkOrderStatus.ClosedId && workOrderResult.Entity.DateClosed?.Date == new DateTime(2000, 1, 1).Date)
            {
                workOrderResult.Entity.DateClosed = DateTime.Now.GetLocalDate(_settings.Timezone).Date;
            }
            _repository.Update(workOrderResult.Entity);

            _log.InfoFormat("Repository update workorder. Name {0}, ID {1}", workorder.Name, workorder.Id);
            _hub.UpdateWorkOrder(model);

            if (createAssignmentResult.IsSucceed)
                return Success();
            else
                return Error(createAssignmentResult.ErrorMessage, createAssignmentResult.ErrorMessage);
        }

        private void ResolveWorkOrderItems(WorkOrderModel model)
        {
            var workOrderItems = Mapper.Map<List<SageWorkOrderItem>>(model.PartsAndLabors);
            var workOrderFromMongo = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();

            if (workOrderItems != null)
            {
                foreach (var workOrderItem in workOrderItems)
                {
                    if ((workOrderFromMongo.WorkOrderItems != null && workOrderFromMongo.WorkOrderItems.Contains(workOrderItem)))
                    {
                        _sageApiProxy.EditWorkOrderItem(workOrderItem);
                    }
                    else
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
                        if (workOrderItem.WorkOrderItem == 0)
                        {
                            _sageApiProxy.AddWorkOrderItem(workOrderItem);
                        }
                        else
                        {
                            _sageApiProxy.EditWorkOrderItem(workOrderItem);
                        }
                    }
                }


                if (workOrderFromMongo.WorkOrderItems != null)
                {
                    var idsToRemove = new List<long>();

                    foreach (var woItem in workOrderFromMongo.WorkOrderItems)
                    {
                        if (!workOrderItems.Select(x => x.WorkOrderItem).Contains(woItem.WorkOrderItem))
                        {
                            idsToRemove.Add(woItem.WorkOrderItem);
                        }
                    }

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteWorkOrderItems(workOrderFromMongo.WorkOrder, idsToRemove);
                    }
                }
            }
            else
            {
                if (workOrderFromMongo.WorkOrderItems != null)
                {
                    var idsToRemove = workOrderFromMongo.WorkOrderItems.Select(x => x.WorkOrderItem).ToArray();

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteWorkOrderItems(workOrderFromMongo.WorkOrder, idsToRemove);
                    }
                }
            }
        }

        [NonAction]
        private void ResolveWorkOrderNotes(WorkOrderModel model)
        {
            var workOrderNotes = Mapper.Map<List<SageNote>>(model.Notes);
            var workOrderFromMongo = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();

            if (workOrderNotes != null)
            {
                foreach (var workOrderNote in workOrderNotes)
                {
                    if ((workOrderFromMongo.WorkNotes != null
                         && workOrderFromMongo.WorkNotes.Contains(workOrderNote)))
                    {
                        this._sageApiProxy.EditNote(workOrderNote);
                    }
                    else
                    {
                        if (workOrderNote.NOTENBR == 0)
                        {
                            this._sageApiProxy.AddNote(workOrderNote);
                        }
                        else
                        {
                            this._sageApiProxy.EditNote(workOrderNote);
                        }
                    }
                }
                if (workOrderFromMongo.WorkNotes != null)
                {
                    var idsToRemove = new List<long>();

                    foreach (var woNote in workOrderFromMongo.WorkNotes)
                    {
                        if (!workOrderNotes.Select(x => x.NOTENBR).Contains(woNote.NOTENBR))
                        {
                            idsToRemove.Add(woNote.NOTENBR);
                        }
                    }

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteNotes(idsToRemove);
                    }
                }
            }
            else
            {
                if (workOrderFromMongo.WorkNotes != null)
                {
                    var idsToRemove = workOrderFromMongo.WorkNotes.Select(x => x.NOTENBR).ToArray();

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteNotes(idsToRemove);
                    }
                }
            }
        }

        [HttpPost]
        [Route("WorkOrder/AddNote")]
        public ActionResult AddNote(WorkOrderNoteModel model)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            if (workOrder == null)
                return Error("Work Order does not exist", $"There are no Work Orders with ID: {model.WorkOrderId}. workOrder == null");

            var note = Mapper.Map<SageNote>(model);
            var addNoteResult = _sageApiProxy.AddNote(note);
            var getNotesResult = _sageApiProxy.GetNotes(note.TRANSNBR);

            if (addNoteResult.IsSucceed && getNotesResult.IsSucceed && getNotesResult.Entities != null)
            {
                workOrder.WorkNotes = getNotesResult.Entities;
                _repository.Update(workOrder);
                _hub.UpdateSageWorkOrder(workOrder);
            }
            else
            {
                _log.ErrorFormat("Was not able to add note to sage. !result.IsSucceed");
                return Error("Note save failed", $"AddNote method IsSucceed==false. {addNoteResult?.ErrorMessage}");
            }
            return Success();
        }

        [HttpPost]
        [Route("WorkOrder/EditNote")]
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
            return Success();
        }

        [HttpPost]
        [Route("WorkOrder/DeleteNote")]
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
            return Success();
        }

        [HttpGet]
        [Route("WorkOrder/GetNotes/{id}")]
        public ActionResult GetNotes(long id)
        {
            var workorder = this._repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id).SingleOrDefault();
            if (workorder == null)
                return Error("Workorder does not exist", $"There are no Workorder with workorderID: {id}. workorder == null");
            if (workorder.WorkNotes == null)
                return Success("");
            var notes = Mapper.Map<IEnumerable<WorkOrderNoteModel>>(workorder.WorkNotes).OrderBy(x => x.NoteId);
            return Success(notes);
        }

        [HttpGet]
        [Route("WorkOrder/GetAssignment/{id}")]
        public ActionResult GetAssignment(long id)
        {
            var assignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == id).Single();
            return assignment == null ?
                 Error("Workorder does not exist", $"There are no Workorder with workorderID: {id}. workorder == null") :
                 Success(assignment);
        }

        [HttpGet]
        [Route("WorkOrder/GetArchive/{id}")]
        public ActionResult GetArchive(long id)
        {
            var workorder = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == id).SingleOrDefault();
            if (workorder == null)
                return Error("Workorder does not exist", $"There are no Workorder with workorderID: {id}. workorder == null");

            var result = _imageService.CreateArchive(workorder);
            return File(result, "application/octet-stream", $"pictures-{id}.zip");
        }

        [HttpPost]
        [Route("WorkOrder/ChangeImageLocation")]
        public ActionResult ChangeImageLocation(ImageLocationModel model)
        {
            return !_imageService.ChangeImageLocation(model) ?
                Error("Images does not exist", $"There are no images with workorderID: {model.WorkOrderId}. images == null") :
                Success();
        }
    }
}