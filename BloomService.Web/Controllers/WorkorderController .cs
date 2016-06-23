using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BloomService.Web.Infrastructure.Jobs;
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

    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private const int _itemsOnPage = 12;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;

        public WorkorderController(IRepository repository, ISageApiProxy sageApiProxy, IBloomServiceHub hub)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
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
                CallDate = model.Calldate.Date,
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

            //var result = _sageApiProxy.AddWorkOrder(workorder);
            //if (!result.IsSucceed)
            //{
            //    _log.ErrorFormat("Was not able to save workorder to sage. !result.IsSucceed");
            //    return Error("Was not able to save workorder to sage");
            //}

            _repository.Add(workorder);
            _log.InfoFormat("Workorder added to repository. ID: {0}, Name: {1}", workorder.Id, workorder.Name);
            _hub.CreateWorkOrder(workorder);
            return Success();
        }

        [HttpGet]
        [Route("Workorder/{id}")]
        public ActionResult GetWorkorder(string id)
        {
            var workOrder = _repository.Get<SageWorkOrder>(id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Workorderpictures/{id}")]
        public ActionResult GetWorkOrdersPictures(string id)
        {
            var pictures = _repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrderBsonId == id).Single();
            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Workorder")]
        public ActionResult GetWorkorders()
        {
            var minDate = DateTime.Now.AddYears(-1);
            var list = _repository.SearchFor<SageWorkOrder>().OrderByDescending(x => x.DateEntered).Take(500).ToList();
            var result = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(list);
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
                    workorders = model.Direction ? workorders.OrderBy(x => x.CallDate) : workorders.OrderByDescending(x => x.CallDate);
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
            }

            if (entitiesCount > _itemsOnPage)
                workorders = workorders.Skip(model.Index * _itemsOnPage).Take(_itemsOnPage);

            var workorderList = workorders.ToList();
            foreach (var obj in workorderList)
            {
                if (obj.TimeEntered == null) continue;
                obj.CallDate = obj.DateEntered?.Date.Add((TimeSpan)obj.TimeEntered) ?? DateTime.MinValue;
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

        [HttpPost]
        [Route("Workorder/Save")]
        public ActionResult SaveWorkOrder(WorkOrderModel model)
        {
            _log.InfoFormat("Method: SaveWorkOrder. Model ID {0}", model.Id);
            var workorder = new SageWorkOrder()
            {
                ARCustomer = model.Customer,
                Location = model.Location,
                CallType = model.Calltype,
                CallDate = model.Calldate.Date,
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

            var result = _sageApiProxy.EditWorkOrder(workorder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to update workorder to sage. !result.IsSucceed");
                return Error("Was not able to update workorder to sage");
            }

            _repository.Update(workorder);
            _log.InfoFormat("Repository update workorder. Name {0}, ID {1}", workorder.Name, workorder.Id);
            return Success();
            

            
        }
    }
}