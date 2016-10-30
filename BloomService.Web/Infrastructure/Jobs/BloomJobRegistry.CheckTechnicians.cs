using System;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
        public void CheckTechnicians()
        {
            //Check technicians position
            Schedule(() =>
            {
                lock (_checkTechniciansLock)
                {
                    var easternTime = DateTime.Now.GetLocalDate(_settings.Timezone);
                    var techniciansAvailable = _repository.SearchFor<SageEmployee>(x => x.IsAvailable).ToArray();
                    foreach (var technician in techniciansAvailable)
                    {
                        var assigments = _repository.SearchFor<SageAssignment>(x => x.EmployeeId == technician.Employee);
                        var assigment = assigments.OrderByDescending(x => x.ScheduleDate).FirstOrDefault();

                        if (assigment == null || assigment.ScheduleDate.GetValueOrDefault().Date != easternTime.Date)
                            continue;
                        var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == assigment.WorkOrder && x.ScheduleDate != null).SingleOrDefault();
                        if (workorder == null || workorder.ScheduleDate.GetValueOrDefault().Date != easternTime.Date)
                            continue;

                        var wts = (TimeSpan)(easternTime - workorder.ScheduleDate);
                        if (!_lateTechnicians.Any(x => x.Employee == technician.Employee && x.WorkOrder == workorder.WorkOrder) && wts.TotalMinutes >= 10 && wts.TotalMinutes < 30 &&
                           _locationService.Distance(workorder.Latitude, workorder.Longitude,
                               technician.Latitude, technician.Longitude) > 5)
                        {
                            _lateTechnicians.Add(new LateTechnician
                            {
                                Employee = technician.Employee,
                                TenMinutes = true,
                                ThirtyMinutes = false,
                                WorkOrder = workorder.WorkOrder

                            });
                            var message =
                                $"{technician.Name} is more than 5 miles from WO #{workorder.WorkOrder} which is scheduled for 10 minutes from now.";
                            _notification.SendNotification(message);
                            _hub.ShowAlert(new SweetAlertModel()
                            {
                                Message = message,
                                Title = "Attention",
                                Type = "warning"
                            });
                        }

                        if (wts.TotalMinutes >= 30 &&
                            _locationService.Distance(workorder.Latitude, workorder.Longitude,
                                technician.Latitude, technician.Longitude) > 15)
                        {
                            if (_lateTechnicians.Any(x => x.Employee == technician.Employee && x.WorkOrder == workorder.WorkOrder))
                            {
                                var lateTechnician = _lateTechnicians.Find(x => x.Employee == technician.Employee && x.WorkOrder == workorder.WorkOrder);
                                if (lateTechnician.ThirtyMinutes == false)
                                {
                                    lateTechnician.TenMinutes = true;
                                    lateTechnician.ThirtyMinutes = true;
                                    var message =
                                        $"{technician.Name} is more than 15 miles from WO #{workorder.WorkOrder} which is scheduled for 30 minutes from now.";
                                    _notification.SendNotification(message);
                                    _hub.ShowAlert(new SweetAlertModel()
                                    {
                                        Message = message,
                                        Title = "Attention",
                                        Type = "warning"
                                    });

                                }
                            }
                            else
                            {
                                _lateTechnicians.Add(new LateTechnician
                                {
                                    Employee = technician.Employee,
                                    TenMinutes = true,
                                    ThirtyMinutes = true,
                                    WorkOrder = workorder.WorkOrder
                                });
                                var message = $"{technician.Name} is more than 15 miles from WO #{workorder.WorkOrder} which is scheduled for 30 minutes from now.";
                                _notification.SendNotification(message);
                                _hub.ShowAlert(new SweetAlertModel()
                                {
                                    Message = message,
                                    Title = "Attention",
                                    Type = "warning"
                                });
                            }
                        }
                    }
                }
            }).ToRunNow()
            .AndEvery(_settings.CheckTechniciansDelay)
            .Minutes();
        }
    }
}