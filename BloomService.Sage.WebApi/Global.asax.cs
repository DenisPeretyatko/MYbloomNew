using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sage.WebApi
{
    using AutoMapper;
    using Mapping;
    using System.Collections.Generic;
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
            ModelBinders.Binders.Add(typeof(Dictionary<string, string>), new ProportiesModelBinder());

            Mapper.Initialize(
                cfg =>
                {
                    cfg.AddProfile(new SageWebApiMappingProfile());
                });
        }
    }
}
