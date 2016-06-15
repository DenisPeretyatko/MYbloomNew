namespace BloomService.Web
{
    using Infrastructure.Jobs;
    using FluentScheduler;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using App_Start;
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinderConfig.RegisterAllBinders();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); 
            AutoMapperConfig.RegisterMappings();
            JobManager.Initialize(new BloomJobRegistry());
        }
    }
}