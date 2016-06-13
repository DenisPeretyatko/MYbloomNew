using System;
using System.Collections.Generic;

namespace BloomService.Web.Infrastructure.Dependecy
{
    public interface IComponentContainer
    {
        T Get<T>() where T : class;
        object Get(Type serviceType);
        IEnumerable<object> GetAll(Type serviceType);
        IEnumerable<T> GetAll<T>();
        void WireUp(object obj);
    }
}