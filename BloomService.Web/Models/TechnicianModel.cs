using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class TechnicianModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("AvailableDays")]
        public List<AvailableDay> AvailableDays { get; set; }
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }
        [JsonProperty("Picture")]
        public string Picture { get; set; }
        [JsonProperty("Color")]
        public string Color { get; set; }
    }

    public class AvailableDay
    {
        [JsonProperty("_id")] 
        public string Id { get; set; }
        [JsonProperty("resourceId")] 
        public string ResourceId { get; set; }
        [JsonProperty("_end")] 
        public string End { get; set; }
        [JsonProperty("start")] 
        public string Start { get; set; }
        [JsonProperty("title")] 
        public string Title { get; set; }
    }
}