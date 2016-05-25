namespace Sage.WebApi.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    public static class SagePropertyConverter
    {
        public static Dictionary<string, string> ConvertToProperties<TEntity>(TEntity entity) where TEntity : SageEntity
        {
            var properties = (from x in entity.GetType().GetProperties() select x).ToDictionary(
                x => x.Name, 
                x =>
                x.GetGetMethod().Invoke(entity, null) == null ? string.Empty : x.GetGetMethod().Invoke(entity, null).ToString());

            properties.Remove("Id");

            return properties;
        }
    }
}