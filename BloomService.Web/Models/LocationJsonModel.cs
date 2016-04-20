using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class LocationJsonModel
    {
        [JsonProperty("order")]
        public long Order { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public float Lat { get; set; }
        [JsonProperty("lng")]
        public float Lng { get; set; }
    }
}