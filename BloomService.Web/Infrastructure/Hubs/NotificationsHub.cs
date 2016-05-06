using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BloomService.Web.Models;
using Microsoft.AspNet.SignalR;

namespace BloomService.Web.Infrastructure.Hubs
{
    public class NotificationsHub : Hub
    {
        public void GetNotifications(List<NotificationModel> model)
        {
            var list = new List<NotificationModel>();
            if (model == null)
            {
                var json = JsonHelper.GetObjects("getNotifications.json");
                var serializer = new JavaScriptSerializer();
                list = serializer.ConvertToType<IEnumerable<NotificationModel>>(json).ToList();

            }
            else
            {
                model.Last().IsViewed = false;
                model.Add(model.Last());
                list = model;
            }
            Clients.All.updateNotifications(list);
        }
    }
}