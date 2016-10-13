using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.StorageProviders;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Entities.Concrete;
    using Models;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using AutoMapper;

    using BloomService.Web.Infrastructure.Mongo;

    using Infrastructure.SignalR;
    using Infrastructure.Services.Interfaces;
    using Infrastructure.Constants;
    using Domain.Extensions;
    using Infrastructure;
    using System.Configuration;

    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private const int ItemsOnPage = 50;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;
        private readonly IDashboardService _dashboardService;
        private readonly INotificationService _notification;
        private readonly IScheduleService _scheduleService;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IStorageProvider _storageProvider;
        private readonly IImageService _imageService;
        private readonly BloomServiceConfiguration _settings;

        public WorkorderController(IRepository repository, ISageApiProxy sageApiProxy,
            IDashboardService dashboardService, IBloomServiceHub hub, INotificationService notification, IScheduleService scheduleService, IHttpContextProvider httpContextProvider, IStorageProvider storageProvider, IImageService imageService)
        {
            _settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _dashboardService = dashboardService;
            _notification = notification;
            _hub = hub;
            _scheduleService = scheduleService;
            _httpContextProvider = httpContextProvider;
            _storageProvider = storageProvider;
            _imageService = imageService;
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
                CallDate = model.Calldate.GetLocalDate(),
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee.ToString(),
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods,
                JCJob = model.JCJob,
                Contact = model.Contact,
                Equipment = model.Equipment,
            };

            var result = _sageApiProxy.AddWorkOrder(workorder);
            result.Entity.LocationNumber = long.Parse(model.Location);

            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to save workorder to sage. !result.IsSucceed");
                return Error(result.ErrorMassage);
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
                assignment.EmployeeId = employee != null ? employee.Employee : 0;
                assignment.Start = (model.AssignmentDate.Date).Add((model.AssignmentTime).TimeOfDay).ToString();
                assignment.End = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay).AddHours(assignment.EstimatedRepairHours.AsDouble() == 0 ? 1 : assignment.EstimatedRepairHours.AsDouble()).ToString();
                assignment.Color = employee?.Color ?? "";
                assignment.Customer = result.Entity.ARCustomer;
                assignment.Location = result.Entity.Location;

                var locations = _repository.GetAll<SageLocation>().ToArray();
                var itemLocation = locations.FirstOrDefault(l => l.Name == result.Entity.Location);
                result.Entity.ScheduleDate = assignment.ScheduleDate;
                result.Entity.Latitude = itemLocation.Latitude;
                result.Entity.Longitude = itemLocation.Longitude;

                _repository.Add(assignment);
                _hub.CreateAssignment(new MapViewModel()
                {
                    WorkOrder = result.Entity,
                    DateEntered = assignment.ScheduleDate,
                    Employee = employee?.Employee ?? 0,
                    Color = employee?.Color ?? ""
                });
            }

            _repository.Add(result.Entity);
            _log.InfoFormat("Workorder added to repository. ID: {0}, Name: {1}", workorder.Id, workorder.Name);
            _notification.SendNotification(string.Format("{0} was created", workorder.WorkOrder));
            return Success();
        }

        [HttpGet]
        [Route("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.Id == id).Single();

            workOrder.CustomerObj = _repository.SearchFor<SageCustomer>(x => x.Customer == workOrder.ARCustomer).SingleOrDefault();
            workOrder.LocationObj = _repository.SearchFor<SageLocation>(x => x.Location == workOrder.LocationNumber).SingleOrDefault();
            workOrder.CalltypeObj = _repository.SearchFor<SageCallType>(x => x.Description == workOrder.CallType).SingleOrDefault();
            workOrder.ProblemObj = _repository.SearchFor<SageProblem>(x => x.Description == workOrder.Problem).SingleOrDefault();
            workOrder.RateSheetObj = _repository.SearchFor<SageRateSheet>().ToList().Where(x => x.DESCRIPTION.Trim() == workOrder.RateSheet).SingleOrDefault();
            workOrder.EmployeeObj = _repository.SearchFor<SageEmployee>(x => x.Name == workOrder.Employee).SingleOrDefault();
            workOrder.HourObj = _repository.SearchFor<SageRepair>(x => x.Repair == workOrder.EstimatedRepairHours.ToString()).SingleOrDefault();
            workOrder.PermissionCodeObj = _repository.SearchFor<SagePermissionCode>().ToList().Where(x => x.DESCRIPTION.Trim() == workOrder.PermissionCode).SingleOrDefault();
            workOrder.PaymentMethodObj = PaymentMethod.PaymentMethods.FirstOrDefault(x => x.Method == workOrder.PayMethod.Trim());
            workOrder.StatusObj = WorkOrderStatus.Status.Single(x => x.Status == workOrder.Status);
            workOrder.Comments = workOrder.Comments.DecodeSafeHtmlString();
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        [Route("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(long id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == id).SingleOrDefault();
            if (pictures != null)
            {
                pictures.Images = pictures.Images.Where(x=>!x.IsDeleted).OrderBy(x => x.Id).ToList();
            }
            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("EditComment")]
        public ActionResult GetWorkOrdersPictures(EditCommentModel model)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.WorkOrder).SingleOrDefault();
            if (pictures == null)
                return Error("Edit picture comments failed", $"GetWorkOrdersPictures method SageImageWorkOrder==false.");
            pictures.Images.Find(x => x.Id == model.Id && !x.IsDeleted).Description = model.Comment;
            _repository.Update(pictures);
            _hub.UpdateWorkOrderPicture(pictures);
            return Success();
        }

        [HttpGet]
        [Route("Workorder")]
        public ActionResult GetWorkorders()
        {
            //var minDate = DateTime.Now.AddYears(-1);
            var list = _repository.SearchFor<SageWorkOrder>().OrderByDescending(x => x.DateEntered).Take(500).ToList();
            var result = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Workorder/CustomerByLocation")]
        public ActionResult GetCustomerByLocation(string arcustomer)
        {
            if (string.IsNullOrEmpty(arcustomer))
            {
                var customers = _repository.GetAll<SageCustomer>();
                return Json(customers, JsonRequestBehavior.AllowGet);
            }

            var result = _repository.GetAll<SageCustomer>().SingleOrDefault(x => x.Customer == arcustomer);
            if (result == null)
            {
                return Json(new SageLocation(), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Workorder/LocationsByCustomer")]
        public ActionResult GetLocationsByCustomer(string customer)
        {
            if (string.IsNullOrEmpty(customer))
            {
                var locations = _repository.GetAll<SageLocation>();
                return Json(locations, JsonRequestBehavior.AllowGet);
            }
            var result = _repository.GetAll<SageLocation>().Where(x => x.ARCustomer == customer).ToList();
            if (!result.Any())
            {
                result = new List<SageLocation>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Workorder/EquipmentByLocation")]
        public ActionResult GetEquipmentByLocation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Json(new List<SageEquipment>(), JsonRequestBehavior.AllowGet);
            }
            var result = _repository.GetAll<SageEquipment>().Where(x => x.Location == name).ToList();
            foreach (var equipment in result)
            {
                if (equipment.InstallLocation == name)
                    equipment.EquipmentType = string.Format($"{equipment.EquipmentType} - installed");
            }
            if (!result.Any())
            {
                result = new List<SageEquipment>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("WorkorderPage")]
        public ActionResult GetWorkorderPage(WorkorderSortModel model)
        {
            long search = -1;
            long.TryParse(model.Search, out search);
            var workorders = _repository.GetAll<SageWorkOrder>();



            if (!string.IsNullOrEmpty(model.Search))
            {
                workorders = _repository.GetAll<SageWorkOrder>()
                        .Where(x => x.ARCustomer.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Location.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Status.ToLower().Contains(model.Search.ToLower()) ||
                                    x.WorkOrder == search
                        );
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

            if (entitiesCount > ItemsOnPage)
                workorders = workorders.Skip(model.Index * ItemsOnPage).Take(ItemsOnPage);

            var workorderList = workorders.ToList();
            var tmpLocations = workorderList.Select(x => x.Location);
            var locations = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Name)).ToList();
            foreach (var obj in workorderList)
            {
                if (obj.TimeEntered == null) continue;

                // TODO Fix this convertation.
                //obj.CallDate = obj.DateEntered?.Date.Add((DateTime)obj.TimeEntered) ?? DateTime.MinValue;
                obj.CallDate = obj.DateEntered?.Date.Add(((DateTime)obj.TimeEntered).TimeOfDay) ?? DateTime.MinValue;
                var location = locations.FirstOrDefault(x => x.Name == obj.Location);
                if (location != null)
                {
                    obj.Latitude = location.Latitude;
                    obj.Longitude = location.Longitude;
                    obj.Address = string.Join(" ", String.Join(", ", new[] { location.Name, location.Address, location.City, location.State, location.ZIP }.Where(str => !string.IsNullOrEmpty(str))));
                }
            }

            var result = new WorkorderSortViewModel()
            {
                CountPage = entitiesCount % ItemsOnPage == 0 ? entitiesCount / ItemsOnPage : entitiesCount / ItemsOnPage + 1,
                WorkordersList = workorderList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkorderPageCount")]
        public ActionResult GetWorkorderPageCount()
        {
            //var date = new DateTime(year, 0, 0);
            var entitiesCount = _repository.GetAll<SageWorkOrder>().Where(x => !string.IsNullOrEmpty(x.Employee)).Count();
            var countPage = entitiesCount % ItemsOnPage == 0 ? entitiesCount / ItemsOnPage : entitiesCount / ItemsOnPage + 1;
            return Json(countPage, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [Route("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: SaveWorkOrder. Model ID {0}", model.Id);

            if (model.Emploee == 0)
                return Error("Employee is required");
            if (model.Location == null)
                return Error("Location is required");
            if (model.Problem == null)
                return Error("Problem is required");


            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.GetLocalDate(),
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
                Status = model.Status == WorkOrderStatus.ClosedId
                    ? WorkOrderStatus.ById(WorkOrderStatus.ClosedId)
                    : WorkOrderStatus.ById(WorkOrderStatus.OpenId),
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
                _scheduleService.CerateAssignment(editedAssignment);

                var locations = _repository.GetAll<SageLocation>().ToArray();
                var itemLocation = locations.FirstOrDefault(l => l.Name == workOrderResult.Entity.Location);
                workOrderResult.Entity.ScheduleDate = model.AssignmentDate.Date.Add((model.AssignmentTime).TimeOfDay);
                workOrderResult.Entity.Latitude = itemLocation.Latitude;
                workOrderResult.Entity.Longitude = itemLocation.Longitude;
            }

            this.ResolveWorkOrderItems(model);
            this.ResolveWorkOrderNotes(model);

            workOrderResult.Entity.Status = WorkOrderStatus.ById(model.Status);
            workOrderResult.Entity.Id = workorder.Id;
            workOrderResult.Entity.IsValid = true;
            workOrderResult.Entity.WorkOrderItems = _sageApiProxy.GetWorkorderItemsByWorkOrderId(workorder.WorkOrder).Entities;
            workOrderResult.Entity.WorkNotes = this._sageApiProxy.GetNotes(workorder.WorkOrder).Entities;
            _repository.Update(workOrderResult.Entity);

            _log.InfoFormat("Repository update workorder. Name {0}, ID {1}", workorder.Name, workorder.Id);
            _hub.UpdateWorkOrder(model);
            if(model.Status == WorkOrderStatus.ClosedId)
                _hub.ShowAlert(new SweetAlertModel()
                {
                    Message = $"Workorder #{model.WorkOrder} closed",
                    Title = "Workorder completed",
                    Type = "success"
                });
            return Success();
        }

        [NonAction]
        private void ResolveWorkOrderItems(WorkOrderModel model)
        {
            var workOrderItems = Mapper.Map<IEnumerable<SageWorkOrderItem>>(model.PartsAndLabors);
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
                        workOrderItem.WorkOrder = Convert.ToInt32(model.WorkOrder);
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
                        _sageApiProxy.DeleteWorkOrderItems(Convert.ToInt32(workOrderFromMongo.WorkOrder), idsToRemove);
                    }
                }
            }
            else
            {
                if (workOrderFromMongo.WorkOrderItems != null)
                {
                    var idsToRemove = workOrderFromMongo.WorkOrderItems.Select(x => x.WorkOrderItem);

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteWorkOrderItems(Convert.ToInt32(workOrderFromMongo.WorkOrder), idsToRemove);
                    }
                }
            }
        }

        [NonAction]
        private void ResolveWorkOrderNotes(WorkOrderModel model)
        {
            var workOrderNotes = Mapper.Map<IEnumerable<SageNote>>(model.Notes);
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
                        this._sageApiProxy.DeleteNotes(idsToRemove);
                    }
                }
            }
            else
            {
                if (workOrderFromMongo.WorkNotes != null)
                {
                    var idsToRemove = workOrderFromMongo.WorkNotes.Select(x => x.NOTENBR);

                    if (idsToRemove.Any())
                    {
                        _sageApiProxy.DeleteNotes(idsToRemove);
                    }
                }
            }
        }

        [HttpPost]
        [Route("Workorder/markAsReviewed")]
        public ActionResult MarkAsReviewed(string workorderId)
        {
            return Success();
        }

        [HttpPost]
        [Route("WorkOrder/AddNote")]
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
            }
            else
            {
                _log.ErrorFormat("Was not able to add note to sage. !result.IsSucceed");
                return Error("Note save failed", $"AddNote method IsSucceed==false. {addNoteResult?.ErrorMassage}"
                );
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
                return Error("Note save failed", $"EditNote method IsSucceed==false. {editNoteResult?.ErrorMassage}"
                );
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
                return Error("Note delete failed", $"DeleteNote method IsSucceed==false. {deleteNoteResult?.ErrorMassage}"
                );
            }
            return Success();
        }

        [HttpGet]
        [Route("WorkOrder/GetNotes/{id}")]
        public ActionResult GetNotes(string id)
        {
            var notes = this._repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == Convert.ToInt64(id)).SingleOrDefault().WorkNotes;
            if (notes == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var result = Mapper.Map<IEnumerable<WorkOrderNoteModel>>(notes).OrderBy(x => x.NoteId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkOrder/GetAssignment/{id}")]
        public ActionResult GetAssignment(string id)
        {
            var assignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == Convert.ToInt64(id)).Single();
            if (assignment == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(assignment, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkOrder/GetArchive/{id}")]
        public ActionResult GetArchive(string id)
        {
            var workorder = long.Parse(id);
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == workorder).SingleOrDefault();
            var result = _imageService.CreateArchive(pictures);

            return File(result, "application/octet-stream", "archive.zip");
        }

        [HttpPost]
        [Route("WorkOrder/ChangeImageLocation")]
        public ActionResult ChangeImageLocation(ImageLocationModel model)
        {
            if (!_imageService.ChangeImageLocation(model))
                return Error("Images does not exist",
                        $"There are no images with workorderID: {model.WorkOrderId}. images == null");
            return Success();
        }
    }
}