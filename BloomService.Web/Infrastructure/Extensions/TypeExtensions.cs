using System;
using System.Collections.Generic;

namespace BloomService.Web.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetHierarchyTypes(this Type type)
        {
            var currentType = type.BaseType;
            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.BaseType;
            }
        }
    }
}