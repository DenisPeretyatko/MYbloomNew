using BloomService.Web.Infrastructure.ModelBinders;
using BloomService.Web.Models;
using System.Web.Mvc;

namespace BloomService.Web.App_Start
{
    public class ModelBinderConfig
    {
        public static void RegisterAllBinders()
        {
            var jsonModelBinder = new JsonNetModelBinder();

            ModelBinders.Binders.Add(typeof(MapViewModel), jsonModelBinder);
            
        }
    }
}