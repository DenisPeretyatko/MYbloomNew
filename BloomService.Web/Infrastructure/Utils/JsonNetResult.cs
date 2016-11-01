using Newtonsoft.Json.Linq;

namespace BloomService.Web.Infrastructure.Utils
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            this.Settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            Settings.Converters.Add(new EscapeQuoteConverter());
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            var scriptSerializer = JsonSerializer.Create(this.Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }

    public class EscapeQuoteConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString().Replace("'", "`"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = JToken.Load(reader).Value<string>();
            return value.Replace("`", "'");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}