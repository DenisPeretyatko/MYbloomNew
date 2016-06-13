using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace BloomService.Web.Infrastructure.Dependecy.Ninject
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            _ninjectKernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null) ? null : (IController) _ninjectKernel.Get(controllerType);
        }
    }
}