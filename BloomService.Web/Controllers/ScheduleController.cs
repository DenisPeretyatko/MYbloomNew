﻿using System.Web.Management;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using BloomService.Web.Infrastructure.Mongo;

    using Domain.Entities.Concrete;
    using Models;

    using Domain.Extensions;
    using Infrastructure.SignalR;

    public class ScheduleController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;
        private readonly INotificationService _notification;
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IRepository repository, ISageApiProxy sageApiProxy, IBloomServiceHub hub, INotificationService notification, IScheduleService scheduleService)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _hub = hub;
            _notification = notification;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult GetSchedules()
        {
            var lastMonth = DateTime.Now.GetLocalDate().AddMonths(-1);
            var model = new ScheduleViewModel();
            //var assignments = _repository.SearchFor<SageAssignment>(x => x.WorkOrder != 0 && x.DateEntered > lastMonth).OrderByDescending(x => x.DateEntered).ToList();
            var assignments = _repository.SearchFor<SageAssignment>(x => x.WorkOrder != 0 && x.ScheduleDate > lastMonth).OrderByDescending(x => x.ScheduleDate).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList(); 
            var mappedAssignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(assignments);
            foreach (var item in mappedAssignments)
            {
                if (!string.IsNullOrEmpty(item.Employee))
                {
                    var color = employees.FirstOrDefault(e => e.Name == item.Employee);
                    item.Color = color != null? color.Color : "";
                }
            };
            var workorders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open" && x.DateEntered > lastMonth && x.AssignmentId != 0).ToList();
            model.Assigments = mappedAssignments.OrderByDescending(x => x.Id).ToList();
            var mappedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(workorders);
            //model.UnassignedWorkorders = ResolveEstimatedRepairHours(mappedWorkorders, mappedAssignments);
            model.UnassignedWorkorders = mappedWorkorders;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.CerateAssignment(model);
            if (!result)
            {
                return Error("Create assignment failed");
            }
            return Success();
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.DeleteAssignment(model);
            if (!result)
            {
                return Error("Delete assignment failed");
            }
            return Success();      
        }

        [NonAction]
        private IEnumerable<WorkorderViewModel> ResolveEstimatedRepairHours(List<WorkorderViewModel> workOrders, List<AssignmentModel> assignments)
        {
            var resultWorkOrders = new List<WorkorderViewModel>();
            foreach (var workorder in workOrders)
            {
                workorder.EstimatedRepairHours =
                    assignments.Single(x => x.WorkOrder == workorder.WorkOrder).EstimatedRepairHours;
                resultWorkOrders.Add(workorder);
            }
            return resultWorkOrders;
        } 
    }
}