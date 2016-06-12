using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using System.Collections.Generic;
using Sage.WebApi.Mapping;
using System.Web.Routing;
using Sage.WebApi.App_Start;

namespace Sage.WebApi
{
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
