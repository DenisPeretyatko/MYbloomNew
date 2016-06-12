namespace BloomService.Web.Utils
{
    using Newtonsoft.Json;

    using RestSharp;
    using RestSharp.Deserializers;

    public class DynamicJsonDeserializer : IDeserializer
    {
        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public TEntity Deserialize<TEntity>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
    }
}