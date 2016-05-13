﻿namespace BloomService.Web.Controllers
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

        private readonly IEmployeeService employeeSageApService;

        private readonly IWorkOrderService workOrderService;

        public ScheduleController(
            IAssignmentService assignmentService,
            IWorkOrderService workOrderService,
            IEmployeeService employeeService)
        {
            this.assignmentService = assignmentService;
            this.workOrderService = workOrderService;
            this.employeeSageApService = employeeService;
        }

        [GET("Schedule")]
        public ActionResult GetSchedules()
        {
            var model = new ScheduleViewModel();
            var sageAssignments = this.assignmentService.Get().ToList();
            var assignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(sageAssignments);
            var employees = this.employeeSageApService.Get().ToList();
            foreach (var item in assignments)
            {
                var employee = employees.FirstOrDefault(e => e.Name == item.Employee);
                item.EmployeeId = employee != null ? employee.Employee : null;
                var startDate = DateTime.ParseExact(
                    item.ScheduleDate + " " + item.StartTime,
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture);
                item.Start = startDate.ToString();
                item.End = startDate.AddHours(Convert.ToDouble(item.EstimatedRepairHours)).ToString();
            }

            var workorders = this.workOrderService.Get().ToList();
            var unassignedWorkorders =
                workorders.Where(
                    x =>
                    x.Status == "Open"
                    && assignments.All(a => a.WorkOrder != x.WorkOrder.ToString(CultureInfo.InvariantCulture))).ToList();
            model.Assigments = assignments;

            model.UnassignedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(unassignedWorkorders);
            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var databaseAssignment = assignmentService.GetByWorkOrderId(model.WorkOrder);
            var assignmanet = new SagePropertyDictionary
                                  {
                                      { "Assignment", databaseAssignment.Assignment }, 
                                      { "ScheduleDate", model.ScheduleDate.ToShortDateString() }, 
                                      { "Employee", model.Employee }, 
                                      { "WorkOrder", model.WorkOrder }, 
                                      { "EstimatedRepairHours", model.EstimatedRepairHours }, 
                                      { "StartTime", model.ScheduleDate.ToShortTimeString() }, 
                                      { "Enddate", model.EndDate.ToShortDateString() }, 
                                      { "Endtime", model.EndDate.ToShortTimeString() }
                                  };
            var edited = this.assignmentService.Edit(assignmanet);
            
            return this.Json("success", JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            var databaseAssignment = assignmentService.GetByWorkOrderId(model.WorkOrder);
            var assignmanet = new SagePropertyDictionary
                                {
                                    { "Assignment", databaseAssignment.Assignment }, 
                                    { "ScheduleDate", model.ScheduleDate.ToShortDateString() }, 
                                    { "Employee", model.Employee }, 
                                    { "WorkOrder", model.WorkOrder }, 
                                    { "EstimatedRepairHours", model.EstimatedRepairHours }, 
                                    { "StartTime", model.ScheduleDate.ToShortTimeString() }, 
                                    { "Enddate", model.EndDate.ToShortDateString() }, 
                                    { "Endtime", model.EndDate.ToShortTimeString() }
                                };
            var edited = this.assignmentService.Edit(assignmanet);

            return this.Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}