using System;
using BloomService.Web.Models;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.Jobs;
using Common.Logging;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Domain.Entities.Concrete;
using System.Linq;
using System.Web.Mvc;
using BloomService.Domain.Extensions;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly IBloomServiceHub _hub;
        private readonly INotificationService _notification;


        public ScheduleService(IRepository repository, ISageApiProxy sageApiProxy, IBloomServiceHub hub, INotificationService notification)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _hub = hub;
            _notification = notification;
        }

        public bool CerateAssignment(AssignmentViewModel model)
        {
            _log.InfoFormat("Method: CreateAssignment. Model ID {0}", model.Id);
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();

            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();
            databaseAssignment.Employee = employee?.Name ?? "";
            databaseAssignment.ScheduleDate = model.ScheduleDate.ToUniversalTime();
            databaseAssignment.WorkOrder = model.WorkOrder;
            databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
            databaseAssignment.StartTime = model.ScheduleDate.ToUniversalTime();
            databaseAssignment.Enddate = model.EndDate.ToUniversalTime();
            databaseAssignment.Endtime = model.EndDate.ToUniversalTime();

            var edited = _sageApiProxy.EditAssignment(databaseAssignment);
            if (edited.IsSucceed == false)
            {
                _log.ErrorFormat("edited == null. Error.");
                return false;
            }

            databaseAssignment.EmployeeId = employee != null ? employee.Employee : null;
            databaseAssignment.Start = model.ScheduleDate.ToString();
            databaseAssignment.End = model.ScheduleDate.ToUniversalTime().AddHours(databaseAssignment.EstimatedRepairHours.AsDouble()).ToString();
            databaseAssignment.Color = employee?.Color ?? "";

            var workorder = _repository.SearchFor<SageWorkOrder>(w => w.WorkOrder == model.WorkOrder).SingleOrDefault();

            databaseAssignment.Customer = workorder.ARCustomer;
            databaseAssignment.Location = workorder.Location;
            var locations = _repository.GetAll<SageLocation>().ToArray();
            var itemLocation = locations.FirstOrDefault(l => l.Name == workorder.Location);
            workorder.ScheduleDate = databaseAssignment.ScheduleDate;
            workorder.Latitude = itemLocation.Latitude;
            workorder.Longitude = itemLocation.Longitude;

            _repository.Update(databaseAssignment);
            workorder.AssignmentId = null;
            _repository.Update(workorder);


            _hub.CreateAssignment(new MapViewModel()
            {
                WorkOrder = workorder,
                DateEntered = databaseAssignment.ScheduleDate,
                Employee = employee?.Employee ?? "",
                Color = employee?.Color ?? ""
            });

            _notification.SendNotification(string.Format("Workorder {0} assigned to {1}", workorder.Name, employee.Name));
            _log.InfoFormat("DatabaseAssignment added to repository. Employee {0}, Employee ID {1}", databaseAssignment.Employee, databaseAssignment.EmployeeId);

            return true;
        }
    }
}