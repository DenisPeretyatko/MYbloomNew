using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface INotificationService
    {
         void SendNotification(string message);
        List<NotificationModel> GetLastNotifications();
    }
}
