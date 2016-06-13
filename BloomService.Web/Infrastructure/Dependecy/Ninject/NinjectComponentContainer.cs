using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject;

namespace BloomService.Web.Infrastructure.Dependecy.Ninject
{
    public class NinjectComponentContainer : IComponentContainer
    {
        private readonly IKernel _kernel;

        public NinjectComponentContainer(IKernel kernel, IEnumerable<Assembly> assembliesToInspect)
        {
            _kernel = kernel;
            _kernel.Bind<IComponentContainer>().ToConstant(this);
            new NinjectComponentRegistrator(kernel).AutoInjectComponentsFrom(assembliesToInspect);
        }

        public T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        public object Get(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _kernel.GetAll<T>();
        }

        public void WireUp(object obj)
        {
            _kernel.Inject(obj);
        }
    }
}