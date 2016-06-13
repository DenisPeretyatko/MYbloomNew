using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BloomService.Web.Infrastructure.Dependecy.Ninject;
using BloomService.Web.Infrastructure.Extensions;

namespace BloomService.Web.Infrastructure.Dependecy
{
    public abstract class ComponentRegistrator
    {
        public void AutoInjectComponentsFrom(IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies.SelectMany(x => x.GetTypes());
            var enumerable = allTypes as Type[] ?? allTypes.ToArray();
            var componentTypes = enumerable.Where(IsComponent);

            var componentsAndServices = componentTypes.ToDictionary(t => t, t => ServicesImplementedBy(t, enumerable));

            foreach (var componentAndServices in componentsAndServices)
            {
                var implType = componentAndServices.Key;
                var serviceTypes = componentAndServices.Value;
                var singleton =
                    ((InjectableAttribute) implType.GetCustomAttributes(typeof (InjectableAttribute), true)[0])
                        .Singleton;
                Register(implType, serviceTypes, singleton);
            }
        }

        private static IEnumerable<Type> ServicesImplementedBy(Type type, IEnumerable<Type> allTypes)
        {
            return
                new[] {type}.Union(type.GetHierarchyTypes())
                    .Union(type.GetInterfaces())
                    .Where(x => IsTypeIn(x, allTypes));
        }

        private static bool IsTypeIn(Type type, IEnumerable<Type> allTypes)
        {
            var enumerable = allTypes as Type[] ?? allTypes.ToArray();
            return enumerable.Any(t => t == type) ||
                   (type.IsGenericType &&
                    enumerable.Any(t => t.IsGenericTypeDefinition && t == type.GetGenericTypeDefinition()));
        }

        private static bool IsComponent(Type type)
        {
            return !type.IsAbstract && type.GetCustomAttributes(typeof (InjectableAttribute), true).Length > 0;
        }

        protected abstract void Register(Type implType, IEnumerable<Type> serviceTypes, bool singleton);
    }
}