namespace Sage.WebApi.Areas.Api
{
    using System.Web.Mvc;

    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Api_Sm_v1", 
                "api/v1/sm/{action}/{id}", 
                new { controller = "ServiceManagement", action = "{action}", id = UrlParameter.Optional });

            //context.MapRoute(
            //    "Api_Sm_v2", 
            //    "api/v2/sm/{controller}/{action}/{id}", 
            //    new { controller = "ServiceManagementApi", action = "{action}", id = UrlParameter.Optional });

            context.MapRoute(
                "Api_Ar", 
                "api/v1/ar/{action}/{id}", 
                new { controller = "AccountsReceivable", action = "{action}", id = UrlParameter.Optional });
            context.MapRoute(
                "Api_Jc", 
                "api/v1/jc/{action}/{id}", 
                new { controller = "JobCost", action = "{action}", id = UrlParameter.Optional });
            context.MapRoute(
                "Api_Au", 
                "api/v1/Authorization/{action}/{id}", 
                new { controller = "Authorization", action = "{action}", id = UrlParameter.Optional });
        }
    }
}