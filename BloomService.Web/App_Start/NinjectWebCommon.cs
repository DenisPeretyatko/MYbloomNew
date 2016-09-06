using BloomService.Web;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace BloomService.Web
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;

    using BloomService.Web.Infrastructure;
    using BloomService.Web.Infrastructure.Dependecy;
    using BloomService.Web.Infrastructure.Dependecy.Ninject;
    using BloomService.Web.Infrastructure.Mongo;
    using BloomService.Web.Infrastructure.Services;
    using BloomService.Web.Infrastructure.Services.Interfaces;
    using BloomService.Web.Infrastructure.SignalR;
    using BloomService.Web.Infrastructure.SignalR.Implementation;
    using BloomService.Web.Infrastructure.StorageProviders;
    using BloomService.Web.Infrastructure.StorageProviders.Implementation;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using RestSharp;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            var setting = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            var connectionString = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var dbName = ConfigurationManager.AppSettings["MainDb"];
            var sageApiHost = setting.SageApiHost;


            kernel.Bind<IRepository>()
                .To<MongoRepository>()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("dbName", dbName);
            kernel.Bind<BloomServiceConfiguration>().ToConstant(setting);

            kernel.Bind<IHttpContextProvider>().To<HttpContextProvider>();
            kernel.Bind<IRestClient>().To<RestClient>().WithConstructorArgument(sageApiHost);
            kernel.Bind<ILocationService>().To<LocationService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISageApiProxy>().To<SageApiProxy>();
            kernel.Bind<IAuthorizationService>().To<AuthorizationService>();
            kernel.Bind<IDashboardService>().To<DashboardService>();
            kernel.Bind<IScheduleService>().To<ScheduleService>();


            kernel.Bind<IStorageProvider>().To<FileSystemStorageProvider>().WithConstructorArgument("basePath", setting.StorageUrl).WithConstructorArgument("baseUrl", setting.StorageUrl);

            kernel.Bind<IStorageFolder>().To<FileSystemStorageFolder>();
            kernel.Bind<IStorageFile>().To<FileSystemStorageFile>();


            ComponentContainer.Current = new NinjectComponentContainer(
                kernel, 
                new[] { typeof(MongoRepository).Assembly });
            kernel.Bind<IBloomServiceHub>().To<BloomServiceHub>();
            kernel.Bind<INotificationService>().To<NotificationService>();
            kernel.Bind<IHubConnectionContext<dynamic>>()
                .ToMethod(ctx => GlobalHost.ConnectionManager.GetHubContext<BloomServiceHub>().Clients);
        }
    }
}