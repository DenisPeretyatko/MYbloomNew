using System.Web.Management;
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
            var assignments = _repository.SearchFor<SageAssignment>(x => x.WorkOrder != 0 && x.DateEntered > lastMonth && x.IsValid).OrderByDescending(x => x.DateEntered).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList();
            //var mappedEmployees = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employees);
            var mappedAssignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(assignments);
            foreach (var item in mappedAssignments)
            {
                if (!string.IsNullOrEmpty(item.Employee))
                {
                    var emps = employees.FirstOrDefault(e => e.Name == item.Employee);
                    if (emps != null)
                    {
                        item.Color = emps.Color;
                    }
                }
            };
            var workorders = _repository.GetAll<SageWorkOrder>();
            var workOrdersForLastMonth = workorders.Where(x => x.Status == "Open" && x.DateEntered > lastMonth && x.AssignmentId != 0).ToList();
            model.Assigments = mappedAssignments.OrderByDescending(x => x.Id).ToList();
            model.UnassignedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(workOrdersForLastMonth);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.CerateAssignment(model);
            if (result != true)
                return Error();
            return Success();
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.DeleteAssignment(model);
            if (result != true)
                return Error();
            return Success();      
        }
    }
}