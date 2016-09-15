using System;
using System.Linq;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
        public void CheckTechnicians()
        {
            //Check technicians position
            Schedule(() =>
            {
                var timeUtc = DateTime.UtcNow;
                var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                var easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                lock (_checkTechniciansLock)
                {
                    techniciansAvailable = _repository.SearchFor<SageEmployee>(x => x.IsAvailable);
                    foreach (var technician in techniciansAvailable)
                    {
                        var assigments = _repository.SearchFor<SageAssignment>(x => x.EmployeeId == technician.Employee);
                        var assigment = assigments.OrderByDescending(x => x.ScheduleDate).FirstOrDefault();

                        if (assigment == null || assigment.ScheduleDate.GetValueOrDefault().Date != easternTime.Date) continue;
                        var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == assigment.WorkOrder && x.ScheduleDate != null).SingleOrDefault();
                        if (workorder == null || workorder.ScheduleDate.GetValueOrDefault().Date != easternTime.Date) continue;
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
                            _notification.SendNotification($"{technician.Name} is more than 5 miles from WO #{workorder.WorkOrder} which is scheduled for 10 minutes from now.");
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
                                    _notification.SendNotification($"{technician.Name} is more than 15 miles from WO #{workorder.WorkOrder} which is scheduled for 30 minutes from now.");
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
                                _notification.SendNotification($"{technician.Name} is more than 15 miles from WO #{workorder.WorkOrder} which is scheduled for 30 minutes from now.");
                            }
                        }
                    }
                }
            }).ToRunNow().AndEvery(_settings.CheckTechniciansDelay).Seconds();
        }
    }
}