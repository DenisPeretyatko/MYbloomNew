namespace BloomService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using BloomService.Web.Services.Abstract;
    using Domain.Repositories.Abstract;

    [Authorize]
    public class ScheduleController : BaseController
    {

        private readonly IRepository _repository;

        public ScheduleController(IRepository repository)
        {
            _repository = repository;
        }

        [GET("Schedule")]
        public ActionResult GetSchedules()
        {
            var model = new ScheduleViewModel();
            var sageAssignments = _repository.GetAll<SageAssignment>().ToList();
            var assignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(sageAssignments);
            var assignedAs = assignments.Where(x => x.Employee != "");
            var employees = _repository.GetAll<SageEmployee>().ToList();
            foreach (var item in assignedAs)
            {
                var employee = employees.FirstOrDefault(e => e.Name == item.Employee);
                item.EmployeeId = employee != null ? employee.Employee : null;
                var startDate = DateTime.ParseExact(
                    item.ScheduleDate + " " + item.StartTime,
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture);
                item.Start = startDate.ToString();
                item.End = startDate.AddHours(Convert.ToDouble(item.EstimatedRepairHours)).ToString();
                item.Color = employee.Color ?? "";
            }

            var workorders = _repository.GetAll<SageWorkOrder>().ToList();
            foreach (var item in assignedAs)
            {
                var workorder = workorders.FirstOrDefault(w => w.WorkOrder == item.WorkOrder);
                if (workorder != null)
                {
                    item.Customer = workorder.ARCustomer;
                    item.Location = workorder.Location;
                }
            }
            var unassignedAs = assignments.Where(x => x.Employee == "");
            var unassignedWorkorders =
                workorders.Where(
                    x =>
                        x.Status == "Open"
                        && unassignedAs.Any(a => a.WorkOrder == x.WorkOrder)).ToList();
            model.Assigments = assignments;

            model.UnassignedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(unassignedWorkorders);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var d = model.ScheduleDate.ToUniversalTime();
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var assignmanet = new SagePropertyDictionary
                                  {
                                      { "Assignment", databaseAssignment.Assignment }, 
                                      { "ScheduleDate", model.ScheduleDate.ToUniversalTime().ToString("yyyy-MM-dd") }, 
                                      { "Employee", model.Employee }, 
                                      { "WorkOrder", model.WorkOrder }, 
                                      { "EstimatedRepairHours", model.EstimatedRepairHours }, 
                                      { "StartTime", model.ScheduleDate.ToUniversalTime().TimeOfDay.ToString() }, 
                                      { "Enddate", model.EndDate.ToUniversalTime().ToString("yyyy-MM-dd") }, 
                                      { "Endtime", model.EndDate.ToUniversalTime().TimeOfDay.ToString() }
                                  };
            var edited = this.assignmentService.Edit(assignmanet);
            if (edited != null)
            {
                var employees = _repository.GetAll<SageEmployee>();
                var employee = employees.FirstOrDefault(e => e.Employee == model.Employee);
                if (employee != null)
                {
                    var empl = employee.Name;
                    databaseAssignment.Employee = empl;
                }
                databaseAssignment.ScheduleDate = model.ScheduleDate.ToUniversalTime().ToString("yyyy-MM-dd");
                databaseAssignment.WorkOrder = model.WorkOrder;
                databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
                databaseAssignment.StartTime = model.ScheduleDate.ToUniversalTime().TimeOfDay.ToString();
                databaseAssignment.Enddate = model.EndDate.ToUniversalTime().ToString("yyyy-MM-dd");
                databaseAssignment.Endtime = model.EndDate.ToUniversalTime().TimeOfDay.ToString();

                _repository.Add(databaseAssignment);
            }

            return this.Json("success", JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            //TODO 
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.Id).Single();
            //if (edited != null)
            //{
                databaseAssignment.Employee = "";
                databaseAssignment.WorkOrder = model.WorkOrder;
                databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
                databaseAssignment.StartTime = model.ScheduleDate.ToUniversalTime().TimeOfDay.ToString();
                databaseAssignment.Enddate = model.EndDate.ToUniversalTime().ToString("yyyy-MM-dd");
                databaseAssignment.Endtime = model.EndDate.ToUniversalTime().TimeOfDay.ToString();

                _repository.Add(databaseAssignment);
            //}

            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();
            _repository.Delete<SageWorkOrder>(workOrder);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}