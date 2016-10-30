using System;
using System.Collections.Generic;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Models;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.Constants;

namespace BloomService.Web.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository _repository;
        private readonly IBloomServiceHub _hub;
        private readonly BloomServiceConfiguration _settings;

        public NotificationService(IRepository repository, 
            IBloomServiceHub hub,
            BloomServiceConfiguration settings)
        {
            _repository = repository;
            _hub = hub;
            _settings = settings;
        }
        
        public void SendNotification(string message)
        {
            var curDate = DateTime.Now.GetLocalDate(_settings.Timezone);
            _repository.Add(new Notification()
            {
                IsViewed = true,
                Date = curDate,
                Time = curDate.TimeOfDay,
                Message = message
            });
           
            _hub.SendNotification(new NotificationModel
            {
                IsViewed = true,
                Message = message,
                Time = $"{curDate.Date.ToString(DateTimeFormat.DateFormat)} {new DateTime(curDate.Ticks).ToString(DateTimeFormat.TimeFormat)}"
            });
        }

        public List<NotificationModel> GetLastNotifications()
        {
            var notifications = _repository.GetAll<Notification>();
            var entitiesCount = notifications.Count();
            if (entitiesCount > _settings.NotificationOnPage)
                notifications = notifications.Skip(entitiesCount - _settings.NotificationOnPage);

            var notificationModels = new List<NotificationModel>();
            foreach (var obj in notifications)
            {
                notificationModels.Add(new NotificationModel()
                {
                    Message = obj.Message,
                    Time = $"{obj.Date.Date.ToString(DateTimeFormat.DateFormat)} {new DateTime(obj.Time.Ticks).ToString(DateTimeFormat.TimeFormat)}", 
                    IsViewed = true
                });
                
            }
            notificationModels.Reverse();
            return notificationModels;
        }
    }
}