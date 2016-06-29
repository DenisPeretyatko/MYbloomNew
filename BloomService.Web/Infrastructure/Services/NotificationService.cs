using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services
{
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
           
            _repository.Add(new Notification()
            {
                IsViewed = true,
                Date = DateTime.Today.Date,
                Time = DateTime.Now.TimeOfDay,
                Message = message
            });

            _hub.SendNotification(new NotificationModel
            {
                IsViewed = true,
                Message = message,
                Time = String.Format("{0} {1}", DateTime.Now.Date.Date.ToString("dd-MM-yyyy"), DateTime.Now.ToString(@"HH\:mm\:ss"))
                });
        }

        public List<NotificationModel> GetLastNotifications()
        {
            var itemsOnPage = 50;
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
                    Time = String.Format("{0} {1}", obj.Date.Date.ToString("dd-MM-yyyy"), obj.Time.ToString(@"hh\:mm\:ss")),
                    IsViewed = true
                });
            }
            result.Reverse();
            return result;
        }

    }
}