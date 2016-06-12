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

    public class ScheduleController : BaseController
    {

        private readonly IRepository _repository;

        public ScheduleController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult GetSchedules()
        {
            var model = new ScheduleViewModel();
            var sageAssignments = _repository.GetAll<SageAssignment>().ToList();
            var assignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(sageAssignments);
            var assignedAs = assignments.Where(x => x.Employee != "");
            var employees = _repository.GetAll<SageEmployee>().ToArray();
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

            var workorders = _repository.GetAll<SageWorkOrder>().ToArray();
            foreach (var item in assignedAs)
            {
                var workorder = workorders.SingleOrDefault(w => w.WorkOrder == item.WorkOrder);
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

        [HttpPost]
        [Route("Schedule/Assignments/Create")]
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
            //TODO add sage api service 
            //var edited = this.assignmentService.Edit(assignmanet);
            var edited = string.Empty;
            var databaseAssignment = assignmentService.GetByWorkOrderId(model.WorkOrder);
            var assignment = Mapper.Map<SageAssignment>(model);
            var edited = assignmentService.Edit(assignment);
            if (edited != null)
            {
                var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();
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

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
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