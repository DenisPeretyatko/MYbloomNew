using BloomService.Web;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BloomService.Web
{
    using System.Configuration;

    using Owin;
    using Infrastructure.Jobs;
    using FluentScheduler;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}