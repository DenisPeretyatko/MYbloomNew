using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly IAssignmentSageApiService _assignmentSageApiService;
        private readonly IWorkOrderSageApiService _workOrderSageApiService;
        private readonly IEmployeeSageApiService _employeeSageApiService;

        public ScheduleController(IAssignmentSageApiService assignmentSageApiService, IWorkOrderSageApiService workOrderSageApiService, IEmployeeSageApiService employeeSageApiService)
        {
            _assignmentSageApiService = assignmentSageApiService;
            _workOrderSageApiService = workOrderSageApiService;
            _employeeSageApiService = employeeSageApiService;
        }
        [GET("Schedule")]
        public ActionResult GetSchedules()
        {
            var model = new ScheduleViewModel();
            var sageAssignments = _assignmentSageApiService.Get().ToList();
            var assignments = AutoMapper.Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(sageAssignments);
            var employees = _employeeSageApiService.Get().ToList();
            foreach (var item in assignments)
            {
                var employee = employees.FirstOrDefault(e => e.Name == item.Employee);
                item.EmployeeId = employee != null? employee.Employee : null;
                var startDate = DateTime.ParseExact(item.ScheduleDate + " " + item.StartTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                item.Start = startDate.ToString();
                item.End = startDate.AddHours(Convert.ToDouble(item.EstimatedRepairHours)).ToString();
            }
           
            var workorders = _workOrderSageApiService.Get().ToList();
            var unassignedWorkorders = workorders.Where(x=> x.Status == "Open" && assignments.All(a=>a.WorkOrder!=x.WorkOrder.ToString(CultureInfo.InvariantCulture))).ToList();
            model.Assigments = assignments;
            
            model.UnassignedWorkorders = AutoMapper.Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(unassignedWorkorders);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [POST("Schedule/Assignments/Create")]
        public ActionResult SaveTecnitianLocations(AssignmentViewModel model)
        {
            var assignmanet = new PropertyDictionary
            {
                {"ScheduleDate", model.ScheduleDate.ToShortDateString()},
                {"Employee", model.Employee},
                {"WorkOrder", model.WorkOrder},
                {"EstimatedRepairHours", model.EstimatedRepairHours},
                {"StartTime", model.ScheduleDate.ToShortTimeString()},
                {"Enddate", model.EndDate.ToShortDateString()},
                {"Endtime", model.EndDate.ToShortTimeString()},
            };

            var created = _assignmentSageApiService.Add(assignmanet);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}