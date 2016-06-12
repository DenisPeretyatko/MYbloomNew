namespace Sage.WebApi.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    public static class SagePropertyConverter
    {
        public static Dictionary<string, string> ConvertToProperties<TEntity>(TEntity entity) where TEntity : SageEntity
        {
            var propertyDictionary = new Dictionary<string, string>();

            var properties = entity.GetType().GetProperties();

            foreach(var property in properties)
            {
                var name = property.Name;
                var value = property.GetGetMethod().Invoke(entity, null);

                if (value != null && !value.ToString().Contains("1/1/0001"))
                {
                    propertyDictionary.Add(name, value.ToString());
                }
            }

            return propertyDictionary;
        }
    }
}