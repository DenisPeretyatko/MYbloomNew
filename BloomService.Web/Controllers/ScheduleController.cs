using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Models;

namespace BloomService.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly ILog _log = LogManager.GetLogger(typeof(ScheduleController));
        private readonly IScheduleService _scheduleService;
        private readonly BloomServiceConfiguration _settings;

        public ScheduleController(IRepository repository, 
            ISageApiProxy sageApiProxy, 
            IScheduleService scheduleService,
            BloomServiceConfiguration settings)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _scheduleService = scheduleService;
            _settings = settings;
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult GetSchedules()
        {
            var lastMonth = DateTime.Now.GetLocalDate(_settings.Timezone).AddMonths(-1);
            var scheduleModel = new ScheduleViewModel();
            var assignments = _repository.SearchFor<SageAssignment>(x => x.WorkOrder != 0 && x.ScheduleDate > lastMonth)
                .OrderByDescending(x => x.ScheduleDate).ToList();
            var employees = _repository.GetAll<SageEmployee>().ToArray(); 
            var mappedAssignments = Mapper.Map<List<SageAssignment>, List<AssignmentModel>>(assignments);
            foreach (var item in mappedAssignments)
            {
                if (!string.IsNullOrEmpty(item.Employee))
                {
                    item.Color = employees.FirstOrDefault(e => e.Name == item.Employee)?
                        .Color ?? "";
                }
            };
            var workorders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open" && x.DateEntered > lastMonth && x.AssignmentId != 0).ToList();
            scheduleModel.Assigments = mappedAssignments.OrderByDescending(x => x.Id).ToList();
            var mappedWorkorders = Mapper.Map<List<SageWorkOrder>, List<WorkorderViewModel>>(workorders);
            scheduleModel.UnassignedWorkorders = mappedWorkorders;
            return Success(scheduleModel);
        }

        [HttpPost]
        [Route("Schedule/Assignments/Create")]
        public ActionResult CreateAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.CerateAssignment(model);
            return !result.IsSucceed ? Error(result.ErrorMessage) : Success();
        }

        [HttpPost]
        [Route("Schedule/Assignments/Delete")]
        public ActionResult DeleteAssignment(AssignmentViewModel model)
        {
            var result = _scheduleService.DeleteAssignment(model);
            return !result ? Error("Delete assignment failed") : Success();
        }

        //[NonAction]
        //private IEnumerable<WorkorderViewModel> ResolveEstimatedRepairHours(List<WorkorderViewModel> workOrders, List<AssignmentModel> assignments)
        //{
        //    var resultWorkOrders = new List<WorkorderViewModel>();
        //    foreach (var workorder in workOrders)
        //    {
        //        workorder.EstimatedRepairHours =
        //            assignments.Single(x => x.WorkOrder == workorder.WorkOrder).EstimatedRepairHours;
        //        resultWorkOrders.Add(workorder);
        //    }
        //    return resultWorkOrders;
        //} 
    }
}