using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sage.WebApi
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // RouteConfig.RegisterRoutes(RouteTable.Routes);
            AttributeRoutingConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(SagePropertyDictionary), new ProportiesModelBinder());
            IoCConfig.Register();
        }
    }
}
