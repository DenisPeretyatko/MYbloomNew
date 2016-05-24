using BloomService.Web;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace BloomService.Web
{
    using System;
    using System.Configuration;
    using System.Web;

    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Managers.Concrete;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Services.Concrete;
    using BloomService.Web.Utils;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Newtonsoft.Json.Linq;

    using Ninject;
    using Ninject.Web.Common;

    using RestSharp;
    using Domain.Extensions;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static string GetAuthToken()
        {
            var username = ConfigurationManager.AppSettings["SageUsername"];
            var password = ConfigurationManager.AppSettings["SagePassword"];
            var sageApiHost = ConfigurationManager.AppSettings["SageApiHost"];
            var restClient = new RestClient(sageApiHost);
            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var json = JObject.Parse(response.Content);
            var result = string.Format("Bearer {0}", json.First.First);
            return result;
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
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
            kernel.Bind<BloomServiceConfiguration>().ToConstant(setting);
            kernel.Bind<IWorkOrderApiManager>().To<WorkOrderApiManager>();
            kernel.Bind<ILocationApiManager>().To<LocationApiManager>();
            kernel.Bind<ICallTypeApiManager>().To<CallTypeApiManager>();
            kernel.Bind<IEmployeeApiManager>().To<EmployeeApiManager>();
            kernel.Bind<IAssignmentApiManager>().To<AssignmentApiManager>();
            kernel.Bind<IDepartmentApiManager>().To<DepartmentApiManager>();
            kernel.Bind<IEquipmentApiManager>().To<EquipmentApiManager>();
            kernel.Bind<IPartApiManager>().To<PartApiManager>();
            kernel.Bind<IProblemApiManager>().To<ProblemApiManager>();
            kernel.Bind<IRepairApiManager>().To<RepairApiManager>();
            kernel.Bind<ICustomerApiManager>().To<CustomerApiManager>();

            var sageApiHost = setting.SageApiHost;

            kernel.Bind<IRestClient>().To<RestClient>().WithConstructorArgument(sageApiHost);
            kernel.Bind<IToken>().To<SageAuthorisationToken>().WithConstructorArgument(GetAuthToken());
            kernel.Bind<IUnitOfWork>().To<MongoDbUnitOfWork>();
            kernel.Bind<IWorkOrderService>().To<WorkOrderService>();
            kernel.Bind<ILocationService>().To<LocationService>();
            kernel.Bind<ICallTypeService>().To<CallTypeService>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IAssignmentService>().To<AssignmentService>();
            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IEquipmentService>().To<EquipmentService>();
            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IPartService>().To<PartService>();
            kernel.Bind<IProblemService>().To<ProblemService>();
            kernel.Bind<IRepairService>().To<RepairService>();
            kernel.Bind<ICustomerService>().To<CustomerService>();

            kernel.Bind<IApiMobileService>().To<ApiMobileService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}