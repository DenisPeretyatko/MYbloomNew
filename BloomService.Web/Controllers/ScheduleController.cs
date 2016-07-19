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
            var assignments = _repository.SearchFor<SageAssignment>(x => !string.IsNullOrEmpty(x.WorkOrder) && x.DateEntered > lastMonth).OrderByDescending(x => x.DateEntered).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList();
            //var mappedEmployees = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employees);
            var mappedAssignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(assignments);
            foreach (var item in mappedAssignments) {
                if (!string.IsNullOrEmpty(item.Employee))
                {
                    item.Color = employees.FirstOrDefault(e => e.Name == item.Employee).Color;
                }
            };
            var workorders = _repository.GetAll<SageWorkOrder>();
            var workOrdersForLastMonth = workorders.Where(x => x.Status == "Open" && x.DateEntered > lastMonth && !string.IsNullOrEmpty(x.AssignmentId)).ToList();
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
            _log.InfoFormat("Method: DeleteAssignment. Model ID {0}", model.Id);
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();
            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();
            var result = _sageApiProxy.UnassignWorkOrders(model.WorkOrder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("!result.IsSucceed. Error");
                return Error();
            }
            databaseAssignment.Employee = "";
            databaseAssignment.EmployeeId = null;
            databaseAssignment.Start = "";
            databaseAssignment.End = "";
            databaseAssignment.Color = "";
            databaseAssignment.Customer = "";
            databaseAssignment.Location = "";
            _repository.Update(databaseAssignment);
            _log.InfoFormat("Deleted from repository: databaseAssignment ID {0}", databaseAssignment.Id);
            workOrder.Employee = "";
            workOrder.ScheduleDate = null;
            workOrder.AssignmentId = databaseAssignment.Assignment;
            _repository.Update(workOrder);
            _log.InfoFormat("Repository update workorder. Name: {0}, Id: {1}", workOrder.Name, workOrder.Id);
            _notification.SendNotification(string.Format("Workorder {0} unassigned from {1}", workOrder.Name, employee.Name));
            _hub.DeleteAssigment(model);
            return Success();
        }
    }
}