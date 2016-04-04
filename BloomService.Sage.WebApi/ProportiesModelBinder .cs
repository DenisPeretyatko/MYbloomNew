using System.IO;
using System.Web.Mvc;

namespace Sage.WebApi
{
    using BloomService.Domain.Entities;

    public class ProportiesModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            if (!IsJSONRequest(controllerContext))
            {
                return base.BindModel(controllerContext, bindingContext);
            }
            var proporties = new Properties();
            var request = controllerContext.HttpContext.Request;
            string result = string.Empty;
            using (var s = new StreamReader(controllerContext.HttpContext.Request.InputStream))
            {
                s.BaseStream.Position = 0;
                result = s.ReadToEnd();
            }

            var body = result.Trim('{', '}');
            foreach (var property in body.Split(','))
            {
                var proportyFields = property.Split(':');
                if (proportyFields.Length < 2)
                    continue;
                var value = string.Empty;
                for (var i = 1; i < proportyFields.Length; i++)
                {
                    value += proportyFields[i];
                    if (i != proportyFields.Length - 1)
                        value += ":";
                }
                var replaceSympol = new char[] { '\"', '\n', '\t', '\r', ' ', '\t' };
                proporties.Add(proportyFields[0].Trim(replaceSympol), value.Trim(replaceSympol));
            }
            return proporties;
        }

        private static bool IsJSONRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/json");
        }
    }
}