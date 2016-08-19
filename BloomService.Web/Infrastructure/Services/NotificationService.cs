using System;
using System.Collections.Generic;
using System.Linq;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services
{
    using BloomService.Web.Infrastructure.Mongo;
    using Constants;
    public class NotificationService : INotificationService
    {
        private readonly IRepository _repository;
        private readonly IBloomServiceHub _hub;

        public NotificationService(IRepository repository, IBloomServiceHub hub)
        {
            _repository = repository;
            _hub = hub;
        }
        
        public void SendNotification(string message)
        {
            var curDate = DateTime.Now.GetLocalDate();
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
                Time = String.Format("{0} {1}", curDate.Date.ToString(DateTimeFormat.DateFormat), curDate.ToString(DateTimeFormat.TimeFormat))
            });
        }

        public List<NotificationModel> GetLastNotifications()
        {
            const int itemsOnPage = 9;
            var notifications = _repository.GetAll<Notification>();
            var entitiesCount = notifications.Count();
            if (entitiesCount > itemsOnPage)
                notifications = notifications.Skip(entitiesCount - itemsOnPage);
            var result = new List<NotificationModel>();
            foreach (var obj in notifications)
            {
                result.Add(new NotificationModel()
                {
                    Message = obj.Message,
                    Time = String.Format("{0} {1}", obj.Date.Date.ToString(DateTimeFormat.DateFormat), obj.Time.ToString(DateTimeFormat.TimeFormat)), 
                    IsViewed = true
                });
                
            }
            result.Reverse();
            return result;
        }
    }
}