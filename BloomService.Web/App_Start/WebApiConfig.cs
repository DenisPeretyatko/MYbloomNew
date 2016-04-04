namespace BloomService.Web
{
    using System.Configuration;
    using System.Web.Http;

    using CacheCow.Server;
    using CacheCow.Server.EntityTagStore.MongoDb;

    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                "API Default", 
                "api/{controller}/{id}", 
                new { id = RouteParameter.Optional });

            var connectionString = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
            var eTagStore = new MongoDbEntityTagStore(connectionString);
            var cacheHandler = new CachingHandler(eTagStore);
            configuration.MessageHandlers.Add(cacheHandler);
        }
    }
}