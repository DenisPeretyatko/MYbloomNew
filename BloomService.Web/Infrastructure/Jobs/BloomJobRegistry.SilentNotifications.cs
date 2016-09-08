using System;
using System.Collections.Generic;
using BloomService.Domain.Entities.Concrete;
using MoonAPNS;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
        public void SendNotifications()
        {  
            //Send silent push notifications to iOS
            Schedule(() =>
            {
                lock (_iosPushNotificationLock)
                {
                    var path = _httpContextProvider.MapPath(_settings.SertificateUrl);
                    technicians = _repository.SearchFor<SageEmployee>(x => x.IsAvailable && !string.IsNullOrEmpty(x.IosDeviceToken));

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

                        var p = new List<NotificationPayload>();
                        p.Add(notificationPayload);
                        var push = new PushNotification(false, path, null) { P12File = path };
                        push.SendToApple(p);
                        _log.InfoFormat("push notification send to {0} at {1}", technician.IosDeviceToken, DateTime.Now);
                        //    }
                        //}
                        //}
                    }
                }
            }).ToRunNow().AndEvery(_settings.NotificationDelay).Seconds();
        }
    }
}