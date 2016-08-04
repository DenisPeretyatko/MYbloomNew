using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
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
    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private const int _itemsOnPage = 12;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;
        private readonly IDashboardService _dashboardService;
        private readonly INotificationService _notification;
        private readonly IScheduleService _scheduleService;

        public WorkorderController(IRepository repository, ISageApiProxy sageApiProxy,
            IDashboardService dashboardService, IBloomServiceHub hub, INotificationService notification, IScheduleService scheduleService)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _dashboardService = dashboardService;
            _notification = notification;
            _hub = hub;
            _scheduleService = scheduleService;
        }

        [HttpPost]
        [Route("Workorder/Create")]
        public ActionResult CreateWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: CreateWorkOrder. Model ID {0}", model.Id);
            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.GetLocalDate(),
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee.ToString(),
                Equipment = 0,
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods
            };

            var result = _sageApiProxy.AddWorkOrder(workorder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to save workorder to sage. !result.IsSucceed");
                return Error("Was not able to save workorder to sage");
            }

            var assignment = result.RelatedAssignment;
            if (string.IsNullOrEmpty(model.Emploee.ToString()))
            {
                result.Entity.AssignmentId = assignment.Assignment;
                _repository.Add(assignment);
            }
            else
            {
                var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Emploee).SingleOrDefault();
                assignment.EmployeeId = employee != null ? employee.Employee : 0;
                assignment.Start = ((DateTime)assignment.ScheduleDate).Add(((DateTime)assignment.StartTime).TimeOfDay).ToString();
                assignment.End = ((DateTime)assignment.ScheduleDate).Add(((DateTime)assignment.StartTime).TimeOfDay).AddHours(assignment.EstimatedRepairHours.AsDouble() == 0 ? 1 : assignment.EstimatedRepairHours.AsDouble()).ToString();
                assignment.Color = employee?.Color ?? "";
                assignment.Customer = result.Entity.ARCustomer;
                assignment.Location = result.Entity.Location;

                var locations = _repository.GetAll<SageLocation>().ToArray();
                var itemLocation = locations.FirstOrDefault(l => l.Name == result.Entity.Location);
                result.Entity.ScheduleDate = assignment.ScheduleDate;
                result.Entity.Latitude = itemLocation.Latitude;
                result.Entity.Longitude = itemLocation.Longitude;

                _repository.Add(assignment);
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
            var workOrder = _repository.Get<SageWorkOrder>(id);

            workOrder.CustomerObj = _repository.SearchFor<SageCustomer>(x => x.Customer == workOrder.ARCustomer).SingleOrDefault();
            workOrder.LocationObj = _repository.SearchFor<SageLocation>(x => x.Name == workOrder.Location && x.ARCustomer == workOrder.ARCustomer).SingleOrDefault();
            workOrder.CalltypeObj = _repository.SearchFor<SageCallType>(x => x.Description == workOrder.CallType).SingleOrDefault();
            workOrder.ProblemObj = _repository.SearchFor<SageProblem>(x => x.Description == workOrder.Problem).SingleOrDefault();
            workOrder.RateSheetObj = _repository.SearchFor<SageRateSheet>().ToList().Where(x => x.DESCRIPTION.Trim() == workOrder.RateSheet).SingleOrDefault();
            workOrder.EmployeeObj = _repository.SearchFor<SageEmployee>(x => x.Name == workOrder.Employee).SingleOrDefault();
            workOrder.HourObj = _repository.SearchFor<SageRepair>(x => x.Repair == workOrder.EstimatedRepairHours.ToString()).SingleOrDefault();
            workOrder.PermissionCodeObj = _repository.SearchFor<SagePermissionCode>().ToList().Where(x => x.DESCRIPTION.Trim() == workOrder.PermissionCode).SingleOrDefault();
            workOrder.PaymentMethodObj = PaymentMethod.PaymentMethods.FirstOrDefault(x => x.Method == workOrder.PayMethod.Trim());
            workOrder.StatusObj = WorkOrderStatus.Status.FirstOrDefault(x => x.Status == workOrder.Status);

            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(long id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == id).SingleOrDefault();
            if (pictures != null)
            {
                pictures.Images = pictures.Images.OrderBy(x => x.Id).ToList();
            }
            return Json(pictures, JsonRequestBehavior.AllowGet);
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
                case "status":
                    workorders = model.Direction ? workorders.OrderBy(x => x.Status) : workorders.OrderByDescending(x => x.Status);
                    break;
                case null:
                    workorders = workorders.OrderByDescending(x => x.ScheduleDate);
                    break;
            }

            if (entitiesCount > _itemsOnPage)
                workorders = workorders.Skip(model.Index * _itemsOnPage).Take(_itemsOnPage);

            var workorderList = workorders.ToList();
            foreach (var obj in workorderList)
            {
                if (obj.TimeEntered == null) continue;

                // TODO Fix this convertation.
                //obj.CallDate = obj.DateEntered?.Date.Add((DateTime)obj.TimeEntered) ?? DateTime.MinValue;
                obj.CallDate = obj.DateEntered?.Date.Add(((DateTime)obj.TimeEntered).TimeOfDay) ?? DateTime.MinValue;
            }

            var result = new WorkorderSortViewModel()
            {
                CountPage = entitiesCount % _itemsOnPage == 0 ? entitiesCount / _itemsOnPage : entitiesCount / _itemsOnPage + 1,
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
            var countPage = entitiesCount % _itemsOnPage == 0 ? entitiesCount / _itemsOnPage : entitiesCount / _itemsOnPage + 1;
            return Json(countPage, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [Route("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: SaveWorkOrder. Model ID {0}", model.Id);
            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.GetLocalDate(),
                Problem = model.Problem,
                RateSheet = model.Ratesheet,
                Employee = model.Emploee.ToString(),
                Equipment = 0,
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods,
                WorkOrder = model.WorkOrder,
                Id = model.Id,
                Status = model.Status == 3 ? WorkOrderStatus.Status.FirstOrDefault(x => x.Value == model.Status).Status : WorkOrderStatus.Status.FirstOrDefault(x => x.Value == 0).Status
            };

            var workOrderResult = _sageApiProxy.EditWorkOrder(workorder);

            if (!workOrderResult.IsSucceed)
            {
                _log.ErrorFormat("Was not able to update workorder to sage. !result.IsSucceed");
                return Error("Was not able to update workorder to sage");
            }

            if (!string.IsNullOrEmpty(model.Emploee.ToString()))
            {
                var assignmentDb = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
                var editedAssignment = new AssignmentViewModel();
                editedAssignment.Employee = model.Emploee;
                editedAssignment.EndDate = (DateTime)assignmentDb.Enddate;
                editedAssignment.EstimatedRepairHours = assignmentDb.EstimatedRepairHours.AsDouble() == 0 ? "1.00" : assignmentDb.EstimatedRepairHours;
                editedAssignment.Id = assignmentDb.Id;
                editedAssignment.ScheduleDate = (DateTime)assignmentDb.StartTime;
                editedAssignment.WorkOrder = assignmentDb.WorkOrder;
                _scheduleService.CerateAssignment(editedAssignment);

                var locations = _repository.GetAll<SageLocation>().ToArray();
                var itemLocation = locations.FirstOrDefault(l => l.Name == workOrderResult.Entity.Location);
                workOrderResult.Entity.ScheduleDate = (DateTime)assignmentDb.StartTime;
                workOrderResult.Entity.Latitude = itemLocation.Latitude;
                workOrderResult.Entity.Longitude = itemLocation.Longitude;
            }

            var workOrderItems = Mapper.Map<IEnumerable<SageWorkOrderItem>>(model.Equipment);

            var workOrderFromMongo = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == workorder.WorkOrder).Single();
            
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
                var idsToRemove = workOrderFromMongo.WorkOrderItems.Select(x => x.WorkOrderItem);

                if (idsToRemove.Any())
                {
                    _sageApiProxy.DeleteWorkOrderItems(Convert.ToInt32(workOrderFromMongo.WorkOrder), idsToRemove);
                }
            }
            
            workOrderResult.Entity.Status = WorkOrderStatus.Status.FirstOrDefault(x => x.Value == model.Status).Status;
            workOrderResult.Entity.Id = workorder.Id;
            workOrderResult.Entity.WorkOrderItems = _sageApiProxy.GetWorkorderItemsByWorkOrderId(workorder.WorkOrder).Entities;
            _repository.Update(workOrderResult.Entity);

            _log.InfoFormat("Repository update workorder. Name {0}, ID {1}", workorder.Name, workorder.Id);
            _hub.UpdateWorkOrder(model);
            return Success();
        }
    }
}