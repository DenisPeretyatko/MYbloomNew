using BloomService.Domain.UnitOfWork;

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

    public class ScheduleController : BaseController
    {
        private readonly IAssignmentService assignmentService;

        private readonly IEmployeeService employeeService;

        private readonly IWorkOrderService workOrderService;

        private readonly IUnitOfWork unitOfWork;

        public ScheduleController(
            IAssignmentService assignmentService,
            IWorkOrderService workOrderService,
            IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            this.assignmentService = assignmentService;
            this.workOrderService = workOrderService;
            this.employeeService = employeeService;
            this.unitOfWork = unitOfWork;
        }

        [GET("Schedule")]
        public ActionResult GetSchedules()
        {
            var model = new ScheduleViewModel();
            var sageAssignments = assignmentService.Get().ToList();
            var assignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(sageAssignments);
            var assignedAs = assignments.Where(x => x.Employee != "");
            var employees = employeeService.Get().ToList();
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

            var workorders = workOrderService.Get().ToList();
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
            var databaseAssignment = assignmentService.GetByWorkOrderId(model.WorkOrder);
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
                var employees = employeeService.Get().ToList();
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

                unitOfWork.GetEntities<SageAssignment>().Add(databaseAssignment);
            }

            return this.Json("success", JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            //TODO 
            var databaseAssignment = assignmentService.GetByWorkOrderId(model.WorkOrder);
            //if (edited != null)
            //{
                databaseAssignment.Employee = "";
                databaseAssignment.WorkOrder = model.WorkOrder;
                databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
                databaseAssignment.StartTime = model.ScheduleDate.ToUniversalTime().TimeOfDay.ToString();
                databaseAssignment.Enddate = model.EndDate.ToUniversalTime().ToString("yyyy-MM-dd");
                databaseAssignment.Endtime = model.EndDate.ToUniversalTime().TimeOfDay.ToString();

                unitOfWork.GetEntities<SageAssignment>().Add(databaseAssignment);
            //}
            
            workOrderService.Delete(model.WorkOrder); 
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}