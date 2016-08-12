using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Services.Interfaces;

using Common.Logging;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace BloomService.Web.Infrastructure.Jobs
{
    using BloomService.Web.Infrastructure.Mongo;
    using MoonAPNS;
    public class BloomJobRegistry : Registry
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly ISageApiProxy _proxy;
        private readonly BloomServiceConfiguration _settings;
        private readonly IRepository _repository;
        private readonly object _iosPushNotificationLock = new object();
        private readonly object _syncSageLock = new object();
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly ILocationService _locationService;
        private readonly object _keepAlive = new object();


        public BloomJobRegistry()
        {
            _proxy = ComponentContainer.Current.Get<ISageApiProxy>();
            _settings = ComponentContainer.Current.Get<BloomServiceConfiguration>();
            _repository = ComponentContainer.Current.Get<IRepository>();
            _httpContextProvider = ComponentContainer.Current.Get<IHttpContextProvider>();
            _locationService = ComponentContainer.Current.Get<ILocationService>();



            //Send silent push notifications to iOS
            Schedule(() =>
            {
                lock (_iosPushNotificationLock)
                {
                    var path = _httpContextProvider.MapPath(_settings.SertificateUrl);
                    var technicians = _repository.SearchFor<SageEmployee>(x => x.IsAvailable && !string.IsNullOrEmpty(x.IosDeviceToken));

                    foreach (var technician in technicians)
                    {
                        //if (technician.AvailableDays != null && technician.AvailableDays.Any())
                        //{
                        //    foreach (var avaibleDay in technician.AvailableDays)
                        //    {
                        //var startTime = avaibleDay.Start.TryAsDateTime();
                        //var endTime = avaibleDay.End.TryAsDateTime();
                        //if (startTime != null && endTime != null && DateTime.Now > startTime && DateTime.Now < endTime)
                        //{

                       var notificationPayload = new NotificationPayload(technician.IosDeviceToken);

                        if (_settings.AlertNotificationEnabled)
                        {
                            notificationPayload = new NotificationPayload(technician.IosDeviceToken, _settings.NotificationAlert);
                        }

                        if (_settings.AlertBadgeNotificationEnabled)
                        {
                            notificationPayload = new NotificationPayload(technician.IosDeviceToken, _settings.NotificationAlert, _settings.NotificationBadge);
                        }

                        if (_settings.AlertBadgeSoundNotificationEnabled)
                        {
                            notificationPayload = new NotificationPayload(technician.IosDeviceToken, _settings.NotificationAlert, _settings.NotificationBadge, _settings.NotificationSound);
                        }

                        //var notificationPayload = new NotificationPayload(technician.IosDeviceToken,"default");
                        var p = new List<NotificationPayload>();
                        p.Add(notificationPayload);
                        PushNotification push = new PushNotification(false, path, null);
                        push.P12File = path;
                        push.SendToApple(p);
                        _log.InfoFormat("push notification send to {0} at {1}", technician.IosDeviceToken, DateTime.Now.ToString());
                        //    }
                        //}
                        //}
                    }
                }
            }).ToRunNow().AndEvery(_settings.NotificationDelay).Seconds();

            //Send request
            Schedule(() =>
            {
                lock (_keepAlive)
                {
                    try
                    {
                        WebRequest req = WebRequest.Create(_settings.SiteUrl);
                        WebResponse result = req.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Keep alive job failed {0}", ex);
                    }

                }
            }).ToRunNow().AndEvery(10).Minutes();

            //Sync sage and mongoDb
            Schedule(() =>
            {
                lock (_syncSageLock)
                {
                    try
                    {
                        var rateSheetArray = _proxy.GetRateSheets();
                        foreach (var entity in rateSheetArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageRateSheet>(x => x.RATESHEETNBR == entity.RATESHEETNBR).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRateSheet {0}", ex);
                    }

                    try
                    {
                        var permissionCodesArray = _proxy.GetPermissionCodes();
                        foreach (var entity in permissionCodesArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SagePermissionCode>(x => x.PERMISSIONCODE == entity.PERMISSIONCODE).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePermissionCode {0}", ex);
                    }

                    try
                    {
                        var workOrders = _proxy.GetWorkorders();
                        foreach (var entity in workOrders.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == entity.WorkOrder).SingleOrDefault();
                            if (mongoEntity == null)
                            {
                                entity.WorkOrderItems = _proxy.GetWorkorderItemsByWorkOrderId(entity.WorkOrder).Entities;
                                _repository.Add(entity);
                            }
                            else
                            {
                                entity.WorkOrderItems = _proxy.GetWorkorderItemsByWorkOrderId(entity.WorkOrder).Entities;
                                entity.Id = mongoEntity.Id;
                                entity.Status = mongoEntity.Status;
                                entity.AssignmentId = mongoEntity.AssignmentId;
                                entity.Latitude = mongoEntity.Latitude;
                                entity.Longitude = mongoEntity.Longitude;

                                entity.ScheduleDate = mongoEntity.ScheduleDate;
                                entity.Color = mongoEntity.Color;
                                entity.EmployeeId = mongoEntity.EmployeeId;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrder {0}", ex);
                    }

                    try
                    {
                        var callTypeArray = _proxy.GetCalltypes();
                        foreach (var entity in callTypeArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageCallType>(x => x.CallType == entity.CallType).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync  SageCallType {0}", ex);
                    }

                    try
                    {
                        var emploees = _proxy.GetEmployees().Entities.Where(x=> !string.IsNullOrEmpty(x.JCJob));
                        foreach (var entity in emploees)
                        {
                            var mongoEntity = _repository.SearchFor<SageEmployee>(x => x.Employee == entity.Employee).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                entity.Color = mongoEntity.Color;
                                entity.Picture = mongoEntity.Picture;
                                entity.AvailableDays = mongoEntity.AvailableDays;
                                entity.IsAvailable = mongoEntity.IsAvailable;
                                entity.IosDeviceToken = mongoEntity.IosDeviceToken;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEmployee {0}", ex);
                    }

                    try
                    {
                        var assignments = _proxy.GetAssignments();
                        foreach (var assigment in assignments.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageAssignment>(x => x.Assignment == assigment.Assignment).SingleOrDefault();
                            var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == assigment.WorkOrder).SingleOrDefault();
                            if (mongoEntity == null)
                            {
                                if (assigment.Employee != "")
                                {
                                    var employee = _repository.SearchFor<SageEmployee>(e => e.Name == assigment.Employee).SingleOrDefault();
                                    assigment.EmployeeId = employee != null ? employee.Employee : null;
                                    var assignmentDate = assigment.ScheduleDate.Value.Add(((DateTime)assigment.StartTime).TimeOfDay);
                                    assigment.Start = assignmentDate.ToString();
                                    assigment.End = assignmentDate.AddHours(assigment.EstimatedRepairHours.AsDouble()).ToString();
                                    assigment.Color = employee?.Color ?? "";
                                    if (workorder != null)
                                    {
                                        assigment.Customer = workorder.ARCustomer;
                                        assigment.Location = workorder.Location;
                                        workorder.ScheduleDate = assignmentDate;
                                        workorder.Color = assigment.Color;
                                        workorder.EmployeeId = assigment.EmployeeId;
                                        _repository.Update(workorder);
                                    }
                                }
                                else
                                {
                                    if (assigment.Employee == "" && workorder != null)
                                    {
                                        workorder.AssignmentId = assigment.Assignment;
                                        _repository.Update(workorder);
                                    }
                                }
                                _repository.Add(assigment);
                            }
                            else
                            {
                                assigment.Id = mongoEntity.Id;
                                assigment.EmployeeId = mongoEntity.EmployeeId;
                                assigment.Start = mongoEntity.Start;
                                assigment.End = mongoEntity.End;
                                assigment.Color = mongoEntity.Color;
                                assigment.Customer = mongoEntity.Customer;
                                assigment.Location = mongoEntity.Location;
                                _repository.Update(assigment);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageAssignment {0}", ex);
                    }

                    try
                    {
                        var equipments = _proxy.GetEquipment();
                        foreach (var entity in equipments.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageEquipment>(x => x.Equipment == entity.Equipment).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEquipment {0}", ex);
                    }

                    try
                    {
                        var problems = _proxy.GetProblems();
                        foreach (var entity in problems.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageProblem>(x => x.Problem == entity.Problem).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageProblem {0}", ex);
                    }

                    try
                    {
                        var repairs = _proxy.GetRepairs();
                        foreach (var entity in repairs.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageRepair>(x => x.Repair == entity.Repair).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRepair {0}", ex);
                    }

                    try
                    {
                        var locations = _proxy.GetLocations();
                        foreach (var entity in locations.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageLocation>(x => x.Location == entity.Location).SingleOrDefault();

                            if (mongoEntity == null)
                            {
                                var woBylocation = _repository.SearchFor<SageWorkOrder>(x => x.Location == entity.Name && x.Status == "Open"); 
                                var hasOpenWorkOrder = woBylocation.Any();
                                if (hasOpenWorkOrder)
                                    _locationService.ResolveLocation(entity);
                                foreach (var wo in woBylocation)
                                {
                                    wo.Latitude = entity.Latitude;
                                    wo.Longitude = entity.Longitude;
                                    _repository.Update(wo);
                                }
                                _repository.Add(entity);
                            }
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                entity.Latitude = mongoEntity.Latitude;
                                entity.Longitude = mongoEntity.Longitude;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocation {0}", ex);
                    }

                    try
                    {
                        var customers = _proxy.GetCustomers();
                        foreach (var entity in customers.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageCustomer>(x => x.Customer == entity.Customer).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageCustomer {0}", ex);
                    }

                    try
                    {
                        var parts = _proxy.GetParts();
                        foreach (var entity in parts.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SagePart>(x => x.Part == entity.Part).SingleOrDefault();
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePart {0}", ex);
                    }
                }
            }).ToRunNow().AndEvery(_settings.SynchronizationDelay).Minutes();
        }
    }
}