using BloomService.Web.Notifications;
using System.Collections.Generic;
using System.Configuration;

namespace BloomService.Web.BackgroundJobs
{
    public static class IOSNotifications
    {
        public static void SendNotifications()
        {
            //var path = ConfigurationManager.AppSettings["devSertificatePath"];
            //var payload1 = new NotificationPayload("e85fe6a013ee9df5b5e7ad9307b9021bdd31aff066587553e0a65f146657a628", "Message", 1, "default",1);

            //var p = new List<NotificationPayload>();
            //p.Add(payload1);
            //PushNotification push = new PushNotification(true, path, null);
            //push.P12File = path;
            //push.SendToApple(p);
        }
    }
}