using BloomService.Web;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BloomService.Web
{
    using System.Configuration;

    using Hangfire;
    using Hangfire.Mongo;

    using Owin;
    using BackgroundJobs;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            var syncDbConnection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var syncDbName = ConfigurationManager.AppSettings["SyncDb"];

            GlobalConfiguration.Configuration.UseMongoStorage(syncDbConnection, syncDbName);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => IOSNotifications.SendNotifications(), ConfigurationManager.AppSettings["delay"]);
        }
    }
}