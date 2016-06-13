using BloomService.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]


namespace BloomService.Web
{
    using System;
    using System.Configuration;
    using System.Web;
    using Services.Abstract;
    using Services.Concrete;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using RestSharp;
    using Domain.Extensions;
    using Domain.Repositories.Abstract;
    using Domain.Repositories.Concrete;
    using Infrastructure.Services.Abstract;
    using Infrastructure.Services.Interfaces;
    using Infrastructure.Services;
    using Infrastructure.Dependecy;
    using Infrastructure.Dependecy.Ninject;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
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

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var setting = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            var connectionString = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var sageApiHost = setting.SageApiHost;

            kernel.Bind<IRepository>().To<MongoRepository>().WithConstructorArgument(connectionString);
            kernel.Bind<BloomServiceConfiguration>().ToConstant(setting);

            kernel.Bind<IHttpContextProvider>().To<HttpContextProvider>();
            kernel.Bind<IRestClient>().To<RestClient>().WithConstructorArgument(sageApiHost);
            kernel.Bind<ILocationService>().To<LocationService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISageApiProxy>().To<SageApiProxy>();
            kernel.Bind<IAuthorizationService>().To<AuthorizationService>();

            ComponentContainer.Current = new NinjectComponentContainer(kernel, new[] {
                    typeof(MongoRepository).Assembly
            });
        }
    }
}