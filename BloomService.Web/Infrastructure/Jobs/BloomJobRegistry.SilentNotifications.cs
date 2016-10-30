using System;
using System.Collections.Generic;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
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
                    var path = _httpContextProvider.MapPath(_settings.PushCertificateUrl);
                    var technicians = _repository.SearchFor<SageEmployee>(x => x.IsAvailable && !string.IsNullOrEmpty(x.IosDeviceToken)).ToArray();

                    foreach (var technician in technicians)
                    {
                        bool shouldUpdateLocation = false;
                        if (technician.AvailableDays != null && technician.AvailableDays.Any())
                        {
                            foreach (var avaliableDay in technician.AvailableDays)
                            {
                                var startTime = avaliableDay.Start.TryAsDateTime() ?? DateTime.MinValue;
                                var endTime = avaliableDay.End.TryAsDateTime() ?? DateTime.MinValue;
                                if (DateTime.Now > startTime && DateTime.Now < endTime)
                                {
                                    shouldUpdateLocation = true;
                                }
                            }
                        }

                        if (shouldUpdateLocation || _settings.IngoreTechnicianAvaliability)
                        {
                            var notificationPayload = new NotificationPayload(technician.IosDeviceToken);
                            if (_settings.AlertNotificationEnabled)
                            {
                                notificationPayload = new NotificationPayload(technician.IosDeviceToken,
                                    _settings.NotificationAlert);
                            }

                            if (_settings.AlertBadgeNotificationEnabled)
                            {
                                notificationPayload = new NotificationPayload(technician.IosDeviceToken,
                                    _settings.NotificationAlert, _settings.NotificationBadge);
                            }

                            if (_settings.AlertBadgeSoundNotificationEnabled)
                            {
                                notificationPayload = new NotificationPayload(technician.IosDeviceToken,
                                    _settings.NotificationAlert, _settings.NotificationBadge,
                                    _settings.NotificationSound);
                            }

                            var notificationPayloads = new List<NotificationPayload>
                            {
                                notificationPayload
                            };
                            var push = new PushNotification(false, path, null)
                            {
                                P12File = path
                            };
                            push.SendToApple(notificationPayloads);

                            _log.InfoFormat("Push notification send to technician {0} on device {1}", technician.Name, technician.IosDeviceToken);
                        }
                    }
                }
            }).ToRunNow()
            .AndEvery(_settings.NotificationDelay)
            .Minutes();
        }
    }
}