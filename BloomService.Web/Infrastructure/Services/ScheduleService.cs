using System;
using System.Data;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Models;
using Common.Logging;

namespace BloomService.Web.Infrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository _repository;
        private readonly ISageApiProxy _sageApiProxy;
        private readonly ILog _log = LogManager.GetLogger(typeof(ScheduleService));
        private readonly IBloomServiceHub _hub;
        private readonly INotificationService _notification;
        private readonly ILocationService _locationService;

        public ScheduleService(IRepository repository, ISageApiProxy sageApiProxy, IBloomServiceHub hub, INotificationService notification, ILocationService locationService)
        {
            _repository = repository;
            _sageApiProxy = sageApiProxy;
            _hub = hub;
            _notification = notification;
            _locationService = locationService;
        }

        public ResponceModel CerateAssignment(AssignmentViewModel model)
        {
            _log.InfoFormat("Method: CreateAssignment. Model ID {0}", model.Id);

            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();

            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();

            if (employee != null && (!employee.IsAvailable || HasСrossoverAssignment(employee.Name, model.ScheduleDate, model.EndDate)) || !CheckEmployeeAvailableTime(employee, model.ScheduleDate, model.EndDate))
                return new ResponceModel
                {
                    IsSucceed = false,
                    ErrorMessage = $"Sorry {employee.Name} is not available at this time."
                };

            databaseAssignment.Employee = employee?.Name ?? "";
            databaseAssignment.ScheduleDate = model.ScheduleDate;
            databaseAssignment.WorkOrder = model.WorkOrder;
            databaseAssignment.EstimatedRepairHours = model.EstimatedRepairHours;
            databaseAssignment.StartTime = model.ScheduleDate;
            databaseAssignment.Enddate = model.EndDate;
            databaseAssignment.Endtime = model.EndDate;

            var edited = _sageApiProxy.EditAssignment(databaseAssignment);
            if (edited.IsSucceed == false)
            {
                _log.ErrorFormat("edited == null. Error.");
                return new ResponceModel
                {
                    IsSucceed = false,
                    ErrorMessage = $"Sage error."
                };
            }

            databaseAssignment.EmployeeId = employee?.Employee ?? 0;
            databaseAssignment.Start = ((DateTime)edited.Entity.ScheduleDate).Add(((DateTime)edited.Entity.StartTime).TimeOfDay).ToString();
            databaseAssignment.End = ((DateTime)edited.Entity.ScheduleDate).Add(((DateTime)edited.Entity.StartTime).TimeOfDay).AddHours(edited.Entity.EstimatedRepairHours.AsDouble()).ToString();
            databaseAssignment.Color = employee?.Color ?? "";

            var workorder = _repository.SearchFor<SageWorkOrder>(w => w.WorkOrder == model.WorkOrder).SingleOrDefault();

            databaseAssignment.Customer = workorder.ARCustomer;
            databaseAssignment.Location = workorder.Location;
            var locations = _repository.GetAll<SageLocation>().ToArray();
            var itemLocation = locations.FirstOrDefault(l => l.Name == workorder.Location);
            if (itemLocation == null)
            {
                itemLocation = new SageLocation();
                _locationService.ResolveLocation(itemLocation);
            }
            else if (itemLocation.Longitude == 0 || itemLocation.Longitude == 0)
            {
                _locationService.ResolveLocation(itemLocation);
            }
            workorder.ScheduleDate = databaseAssignment.ScheduleDate;
            workorder.Latitude = itemLocation.Latitude;
            workorder.Longitude = itemLocation.Longitude;
            workorder.Color = databaseAssignment.Color;
            workorder.EmployeeId = databaseAssignment.EmployeeId;
            workorder.Employee = databaseAssignment.Employee;

            _repository.Update(databaseAssignment);
            workorder.AssignmentId = 0;
            _repository.Update(workorder);
            _repository.Update(itemLocation);


            _hub.CreateAssignment(new MapViewModel()
            {
                WorkOrder = workorder,
                DateEntered = databaseAssignment.ScheduleDate,
                Employee = employee?.Employee ?? 0,
                Color = employee?.Color ?? ""
            });

            _notification.SendNotification(string.Format("Workorder {0} assigned to {1}", workorder.Name, employee.Name));
            _log.InfoFormat("DatabaseAssignment added to repository. Employee {0}, Employee ID {1}", databaseAssignment.Employee, databaseAssignment.EmployeeId);

            return new ResponceModel
            {
                IsSucceed = true,
            };
        }

        public bool DeleteAssignment(AssignmentViewModel model)
        {
            _log.InfoFormat("Method: DeleteAssignment. Model ID {0}", model.Id);
            var databaseAssignment = _repository.SearchFor<SageAssignment>(x => x.WorkOrder == model.WorkOrder).Single();
            var workOrder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.WorkOrder).Single();
            var employee = _repository.SearchFor<SageEmployee>(x => x.Employee == model.Employee).SingleOrDefault();
            var result = _sageApiProxy.UnassignWorkOrders(model.WorkOrder);
            if (!result.IsSucceed)
            {
                _log.ErrorFormat("!result.IsSucceed. Error");
                return false;
            }
            databaseAssignment.Employee = "";
            databaseAssignment.EmployeeId = 0;
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
            _notification.SendNotification($"Workorder {workOrder.Name} unassigned from {employee.Name}");
            _hub.DeleteAssigment(model);

            return true;
        }

        public bool HasСrossoverAssignment(string employeeName, DateTime start, DateTime end)
        {
            var assignments = _repository.SearchFor<SageAssignment>(
                x => x.Employee == employeeName).ToList();
            var crossed = assignments.Count(x => (start > Convert.ToDateTime(x.Start) && start < Convert.ToDateTime(x.End)) ||
            (end < Convert.ToDateTime(x.End) && end > Convert.ToDateTime(x.Start)) ||
            (start < Convert.ToDateTime(x.End) && end > Convert.ToDateTime(x.Start)) ||
            (start > Convert.ToDateTime(x.Start) && end < Convert.ToDateTime(x.End)));
            return crossed != 0;
        }

        public bool CheckEmployeeAvailableTime(SageEmployee employee, DateTime eventStart, DateTime eventEnd)
        {
            foreach (var range in employee.AvailableDays)
            {
                if (eventStart > Convert.ToDateTime(range.Start) && eventStart < Convert.ToDateTime(range.End) ||
            (eventEnd < Convert.ToDateTime(range.End) && eventEnd > Convert.ToDateTime(range.Start)) ||
            (eventStart < Convert.ToDateTime(range.End) && eventEnd > Convert.ToDateTime(range.Start)) ||
            (eventStart > Convert.ToDateTime(range.Start) && eventEnd < Convert.ToDateTime(range.End)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}