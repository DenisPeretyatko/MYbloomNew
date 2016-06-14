using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Extensions;
using BloomService.Web.Infrastructure.Services;
using BloomService.Web.Infrastructure.Services.Abstract;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Notifications;
using BloomService.Web.Services.Abstract;
using Common.Logging;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BloomService.Web.Infrastructure.Jobs
{
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
                        if (technician.AvailableDays != null && technician.AvailableDays.Any())
                        {
                            foreach (var avaibleDay in technician.AvailableDays)
                            {
                                var startTime = avaibleDay.Start.TryAsDateTime();
                                var endTime = avaibleDay.End.TryAsDateTime();
                                if (startTime != null && endTime != null && DateTime.Now > startTime && DateTime.Now < endTime)
                                {
                                    var notificationPayload = new NotificationPayload(technician.IosDeviceToken, "RequestSendLocation", 1, "default", 1);
                                    var p = new List<NotificationPayload>();
                                    p.Add(notificationPayload);
                                    PushNotification push = new PushNotification(true, path, null);
                                    push.P12File = path;
                                    push.SendToApple(p);
                                }
                            }
                        }
                    }
                }
            }).ToRunNow().AndEvery(15).Minutes();

            //Sync sage and mongoDb
            Schedule(() =>
            {
                lock (_syncSageLock)
                {
                    try
                    {
                        var workOrders = _proxy.GetWorkorders();
                        foreach (var entity in workOrders.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == entity.WorkOrder).SingleOrDefault();
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
                        _log.ErrorFormat("Can`t sync SageCallType {0}", ex);
                    }

                    try
                    {
                        var assignments = _proxy.GetAssignments();
                        var testList = assignments.Entities.Where(x => !string.IsNullOrEmpty(x.Workorder));
                        foreach (var assigment in assignments.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageAssignment>(x => x.Assignment == assigment.Assignment).SingleOrDefault();
                            if (mongoEntity == null)
                            {
                                if(assigment.Employee != "")
                                {
                                    var employee = _repository.SearchFor<SageEmployee>(e => e.Name == assigment.Employee).SingleOrDefault();
                                    assigment.EmployeeId = employee != null ? employee.Employee : null;
                                    var assignmentDateString = assigment.ScheduleDate + " " + assigment.StartTime;
                                    var assignmentDate = DateTime.ParseExact(assignmentDateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    assigment.Start = assignmentDate.ToString();
                                    assigment.End = assignmentDate.AddHours(assigment.EstimatedRepairHours.AsDouble()).ToString();
                                    assigment.Color = employee?.Color ?? "";
                                }
                                else
                                {
                                    var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == assigment.Workorder).SingleOrDefault();
                                    if (workorder != null)
                                    {
                                        assigment.Customer = workorder.ARCustomer;
                                        assigment.Location = workorder.Location;
                                        workorder.AssignmentId = assigment.Id;
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
                        var emploees = _proxy.GetEmployees();
                        foreach (var entity in emploees.Entities)
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
                                var hasOpenWorkOrder = _repository.SearchFor<SageWorkOrder>(x => x.Location == entity.Name && x.Status == "Open").Any();
                                if (hasOpenWorkOrder)
                                    _locationService.ResolveLocation(entity);
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
            }).ToRunNow().AndEvery(1).Hours();
        }
    }
}