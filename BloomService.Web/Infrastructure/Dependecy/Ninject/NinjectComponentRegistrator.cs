using System;
using System.Collections.Generic;
using Ninject;

namespace BloomService.Web.Infrastructure.Dependecy.Ninject
{
    public class NinjectComponentRegistrator : ComponentRegistrator
    {
        private readonly IKernel _kernel;

        public NinjectComponentRegistrator(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override void Register(Type implType, IEnumerable<Type> serviceTypes, bool singleton)
        {
            foreach (var serviceType in serviceTypes)
            {
                var binding = _kernel.Bind(serviceType).To(implType);
                if (singleton) binding.InSingletonScope();
            }
        }
    }
}