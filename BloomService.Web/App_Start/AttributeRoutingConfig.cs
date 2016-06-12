using BloomService.Web;

using WebActivator;

[assembly: PreApplicationStartMethod(typeof(AttributeRoutingConfig), "Start")]

namespace BloomService.Web
{
    using System.Web.Routing;

   // using AttributeRouting.Web.Mvc;

    public static class AttributeRoutingConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // See http://github.com/mccalltd/AttributeRouting/wiki for more options.
            // To debug routes locally using the built in ASP.NET development server, go to /routes.axd
           // routes.MapAttributeRoutes();
        }

        public static void Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}