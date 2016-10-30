using System.Web.Mvc;
using BloomService.Web.Infrastructure.ModelBinders;
using BloomService.Web.Models;

namespace BloomService.Web
{
    public class ModelBinderConfig
    {
        public static void RegisterAllBinders()
        {
            var jsonModelBinder = new JsonNetModelBinder();

            ModelBinders.Binders.Add(typeof(MapModel), jsonModelBinder);
            
        }
    }
}