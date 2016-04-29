using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloomService.Web.Startup))]
namespace BloomService.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
