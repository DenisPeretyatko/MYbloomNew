using BloomService.Web.Infrastructure.Jobs;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Domain.Entities.Concrete;
    using Models;
    using Domain.Repositories.Abstract;
    using Infrastructure.Services.Abstract;
    using Domain.Extensions;
    using Infrastructure.SignalR;

    public class ScheduleController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;

        public ScheduleController(IRepository repository, ISageApiProxy sageApiProxy, IBloomServiceHub hub)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _hub = hub;
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult GetSchedules()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var model = new ScheduleViewModel();
            var assignments = _repository.SearchFor<SageAssignment>(x => !string.IsNullOrEmpty(x.WorkOrder)).OrderByDescending(x => x.DateEntered).Take(65).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToList();
            var mappedEmployees = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employees);
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
            _log.InfoFormat("Method: CreateAssignment. Model ID {0}", model.Id);
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();

            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();
            databaseAssignment.Employee = employee?.Name ?? "";
            databaseAssignment.ScheduleDate = model.ScheduleDate.ToUniversalTime();
            databaseAssignment.WorkOrder = model.WorkOrder;
            databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
            databaseAssignment.StartTime = model.ScheduleDate.ToUniversalTime().TimeOfDay;
            databaseAssignment.Enddate = model.EndDate.ToUniversalTime();
            databaseAssignment.Endtime = model.EndDate.ToUniversalTime().TimeOfDay;

            databaseAssignment.EmployeeId = employee != null ? employee.Employee : null;
            databaseAssignment.Start = model.ScheduleDate.ToString();
            databaseAssignment.End = model.ScheduleDate.AddHours(databaseAssignment.EstimatedRepairHours.AsDouble()).ToString();
            databaseAssignment.Color = employee?.Color ?? "";

            var workorder = _repository.SearchFor<SageWorkOrder>(w => w.WorkOrder == model.WorkOrder).SingleOrDefault();

            databaseAssignment.Customer = workorder.ARCustomer;
            databaseAssignment.Location = workorder.Location;

            var edited = _sageApiProxy.EditAssignment(databaseAssignment);
            if (edited.IsSucceed == false)
            {
                _log.ErrorFormat("edited == null. Error.");
                return Error();
            }

            _repository.Add(databaseAssignment);
            _hub.CreateAssignment(databaseAssignment);
            _log.InfoFormat("DatabaseAssignment added to repository. Employee {0}, Employee ID {1}", databaseAssignment.Employee, databaseAssignment.EmployeeId);
            return Success();
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            _log.InfoFormat("Method: DeleteAssignment. Model ID {0}", model.Id);
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();
            var result = _sageApiProxy.UnassignWorkOrders(model.WorkOrder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("!result.IsSucceed. Error");
                return Error();
            }
            databaseAssignment.Employee = "";
            _repository.Update(databaseAssignment);
            _log.InfoFormat("Deleted from repository: databaseAssignment ID {0}", databaseAssignment.Id);
            workOrder.Employee = "";
            _repository.Update(workOrder);
            _log.InfoFormat("Repository update workorder. Name: {0}, Id: {1}", workOrder.Name, workOrder.Id);
            return Success();
        }
    }
}