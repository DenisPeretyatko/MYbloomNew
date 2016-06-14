namespace BloomService.Web
{
    using System.Web.Http;

    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                "API Default", 
                "api/{controller}/{id}", 
                new { id = RouteParameter.Optional});

            //configuration.Routes.MapHttpRoute(
            //    "API Mobile Default", 
            //    "apimobile/{controller}/{id}", 
            //    new { id = RouteParameter.Optional });

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;
        }
    }
}