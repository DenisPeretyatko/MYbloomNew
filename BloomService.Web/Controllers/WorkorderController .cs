using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Entities.Concrete;
    using Models;
    using System.Linq;
    using Domain.Repositories.Abstract;
    using System;
    using Infrastructure.Services.Abstract;
    using System.Collections.Generic;
    using AutoMapper;
    using Infrastructure.SignalR;
    using Infrastructure.Services.Interfaces;
    using Infrastructure.Constants;
    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private const int _itemsOnPage = 12;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;
        private readonly IDashboardService _dashboardService;
        private readonly INotificationService _notification;

        public WorkorderController(IRepository repository, ISageApiProxy sageApiProxy,
            IDashboardService dashboardService, IBloomServiceHub hub, INotificationService notification)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _dashboardService = dashboardService;
            _notification = notification;
            _hub = hub;
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
                Employee = model.Emploee,
                Equipment = Convert.ToUInt16(model.Equipment),
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

            var lookups = _dashboardService.GetLookups();
            workOrder.CustomerObj = Mapper.Map<CustomerModel, SageCustomer>(lookups.Customers.FirstOrDefault(x => x.Customer == workOrder.ARCustomer));
            workOrder.LocationObj = Mapper.Map<LocationModel, SageLocation>(lookups.Locations.FirstOrDefault(x => x.Name == workOrder.Location));
            workOrder.CalltypeObj = Mapper.Map<CallTypeModel, SageCallType>(lookups.Calltypes.FirstOrDefault(x => x.Description == workOrder.CallType));
            workOrder.ProblemObj = Mapper.Map<ProblemModel, SageProblem>(lookups.Problems.FirstOrDefault(x => x.Description == workOrder.Problem));
            workOrder.RateSheetObj = Mapper.Map<RateSheetModel, SageRateSheet>(lookups.RateSheets.FirstOrDefault(x => x.DESCRIPTION.Trim() == workOrder.RateSheet));
            workOrder.EmployeeObj = Mapper.Map<EmployeeModel, SageEmployee>(lookups.Employes.FirstOrDefault(x => x.Name == workOrder.Employee));
            workOrder.HourObj = Mapper.Map<RepairModel, SageRepair>(lookups.Hours.FirstOrDefault(x => x.Repair == workOrder.EstimatedRepairHours));
            workOrder.PermissionCodeObj = Mapper.Map<PermissionCodeModel, SagePermissionCode>(lookups.PermissionCodes.FirstOrDefault(x => x.DESCRIPTION.Trim() == workOrder.PermissionCode));
            workOrder.PaymentMethodObj = PaymentMethod.PaymentMethods.FirstOrDefault(x => x.Method == workOrder.PayMethod.Trim());

            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(string id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrderBsonId == id).SingleOrDefault();
            pictures.Images = pictures.Images.OrderBy(x => x.Id).ToList();
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
            if(!result.Any())
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
            var workorders = _repository.GetAll<SageWorkOrder>();

            if (!string.IsNullOrEmpty(model.Search))
            {
                workorders = _repository.GetAll<SageWorkOrder>()
                        .Where(x => x.ARCustomer.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Location.ToLower().Contains(model.Search.ToLower()) ||
                                    x.Status.ToLower().Contains(model.Search.ToLower()) ||
                                    x.WorkOrder.Contains(model.Search)
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
            var entitiesCount = _repository.GetAll<SageWorkOrder>().Count();
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
                Employee = model.Emploee,
                Equipment = Convert.ToUInt16(model.Equipment),
                EstimatedRepairHours = Convert.ToDecimal(model.Estimatehours),
                NottoExceed = model.Nottoexceed,
                Comments = model.Locationcomments,
                CustomerPO = model.Customerpo,
                PermissionCode = model.Permissiocode,
                PayMethod = model.Paymentmethods,
                WorkOrder = model.WorkOrder,
                Id = model.Id
            };

            var result = _sageApiProxy.EditWorkOrder(workorder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to update workorder to sage. !result.IsSucceed");
                return Error("Was not able to update workorder to sage");
            }

            result.Entity.Id = workorder.Id;
            _repository.Update(result.Entity);
            _log.InfoFormat("Repository update workorder. Name {0}, ID {1}", workorder.Name, workorder.Id);
            _hub.UpdateWorkOrder(model);
            return Success();
        }
    }
}