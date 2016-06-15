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
    public class ScheduleController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;

        public ScheduleController(IRepository repository, ISageApiProxy sageApiProxy)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult GetSchedules()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var model = new ScheduleViewModel();
            var assignments = _repository.SearchFor<SageAssignment>(x => !string.IsNullOrEmpty(x.WorkOrder)).OrderByDescending(x => x.DateEntered).Skip(1400).Take(65).ToList();
            var mappedAssignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(assignments);
            var workorders = _repository.GetAll<SageWorkOrder>();
            var workOrdersForLastMonth = workorders.Where(x => x.Status == "Open" && x.DateEntered > lastMonth && !string.IsNullOrEmpty(x.AssignmentId)).ToList();
            model.Assigments = mappedAssignments.OrderByDescending(x => x.Id);
            model.UnassignedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(workOrdersForLastMonth);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var d = model.ScheduleDate.ToUniversalTime();
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var edited = _sageApiProxy.EditAssignment(databaseAssignment);
            if (edited == null)
                return Error();

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

            _repository.Add(databaseAssignment);

            return Success();
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();
            var result = _sageApiProxy.UnassignWorkOrders(model.WorkOrder);
            if (!result.IsSucceed)
                return Error();

            _repository.Delete(databaseAssignment);
            workOrder.Employee = "0";
            _repository.Update(workOrder);
            return Success();
        }
    }
}