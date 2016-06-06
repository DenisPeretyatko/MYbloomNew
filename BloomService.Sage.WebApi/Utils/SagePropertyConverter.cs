namespace Sage.WebApi.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    public static class SagePropertyConverter
    {
        public static Dictionary<string, string> ConvertToProperties<TEntity>(TEntity entity) where TEntity : SageEntity
        {
            //var properties = (from x in entity.GetType().GetProperties() select x).ToDictionary(
            //    x => x.Name, 
            //    x =>
            //    x.GetGetMethod().Invoke(entity, null) == null ? string.Empty : x.GetGetMethod().Invoke(entity, null).ToString());

            var propertyDictionary = new Dictionary<string, string>();

            var properties = entity.GetType().GetProperties();

            foreach(var property in properties)
            {
                var name = property.Name;
                var value = property.GetGetMethod().Invoke(entity, null);

                if (value != null && value.ToString() != "1/1/0001 5:00:00 AM")
                {
                    propertyDictionary.Add(name, value.ToString());
                }
            }

            return propertyDictionary;
        }
    }
}