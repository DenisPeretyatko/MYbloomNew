using Sage.WebApi.Infratructure.Service;
using Sage.WebApi.Infratructure.Service.Implementation;
using System.Web.Http;
using System.Web.Mvc;
using TinyIoC;

namespace Sage.WebApi
{
    using Sage.WebApi.Infratructure;

    public static class IoCConfig
    {
        public static void Register()
        {
            var container = TinyIoCContainer.Current;
            
            container.Register<IServiceManagement, ServiceManagement>().AsPerRequestSingleton();
            container.Register<IServiceOdbc, ServiceOdbc>().AsPerRequestSingleton();
            container.Register<SageWebConfig>().AsPerRequestSingleton();
            container.Register<ClaimsAgent>().AsPerRequestSingleton();
            DependencyResolver.SetResolver(new TinyIocMvcDependencyResolver(container));
            
            GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
    }
}
