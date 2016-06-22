﻿using System.Linq;
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
    public class WorkorderController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private const int _itemsOnPage = 12;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));

        public WorkorderController(IRepository repository, ISageApiProxy sageApiProxy)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
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

            var result = _sageApiProxy.AddWorkOrder(workorder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("Was not able to save workorder to sage. !result.IsSucceed");
                return Error("Was not able to save workorder to sage");
            }

            _repository.Add(result.Entity);
            _log.InfoFormat("Workorder added to repository. ID: {0}, Name: {1}", result.Entity.Id, result.Entity.Name);
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
            var list = _repository.SearchFor<SageWorkOrder>().OrderByDescending(x=>x.DateEntered).Take(500).ToList();
            var result = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkorderPage/{index}")]
        public ActionResult GetWorkorderPage(int index)
        {
            var entitiesCount = _repository.GetAll<SageWorkOrder>().Count();
            var list = _repository.GetAll<SageWorkOrder>().Skip((index - 1) * _itemsOnPage).Take(_itemsOnPage);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("WorkorderPageCount")]
        public ActionResult GetWorkorderPageCount(int year, string search)
        {
            var date = new DateTime(year, 0, 0);
            var entitiesCount = _repository.SearchFor<SageWorkOrder>(x=>x.CustomerPO.Contains(search) || x.Location.Contains(search)).Count();
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