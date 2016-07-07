using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Routing;

namespace Sage.WebApi
{
    using Sage.WebApi.Infratructure.Mapping;
    using Sage.WebApi.Infratructure.ModelBinders;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ModelBinders.Binders.Add(typeof(Dictionary<string, string>), new ProportiesModelBinder());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg => cfg.AddProfile(new SageWebApiMappingProfile()));
        }
    }
}
