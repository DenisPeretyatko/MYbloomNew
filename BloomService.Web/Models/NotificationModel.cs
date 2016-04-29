using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class NotificationModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("isViewed")]
        public bool IsViewed { get; set; }
    }
}