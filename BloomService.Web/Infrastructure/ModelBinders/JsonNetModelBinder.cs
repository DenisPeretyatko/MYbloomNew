using Newtonsoft.Json;
using System.IO;
using System.Web.Mvc;

namespace BloomService.Web.Infrastructure.ModelBinders
{
    public class JsonNetModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!IsFormUrlEncodedRequest(controllerContext) && !IsJsonRequest(controllerContext))
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            // Get the JSON data that's been posted
            var request = controllerContext.HttpContext.Request;
            //in some setups there is something that already reads the input stream if content type = 'application/json', so seek to the begining
            request.InputStream.Seek(0, SeekOrigin.Begin);
            var jsonStringData = new StreamReader(request.InputStream).ReadToEnd();

            return JsonConvert.DeserializeObject(jsonStringData, bindingContext.ModelMetadata.ModelType);
        }

        private static bool IsJsonRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/json");
        }

        private static bool IsFormUrlEncodedRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/x-www-form-urlencoded");
        }
    }
}