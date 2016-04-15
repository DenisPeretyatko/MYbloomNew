namespace BloomService.Domain.Entities
{
    using Newtonsoft.Json;

    public class TimeStamp
    {
        [JsonProperty("Ticks")]
        public long Ticks { get; set; }
        [JsonProperty("Days")]
        public int Days { get; set; }
        [JsonProperty("Hours")]
        public int Hours { get; set; }
        [JsonProperty("Milliseconds")]
        public int Milliseconds { get; set; }
        [JsonProperty("Minutes")]
        public int Minutes { get; set; }
        [JsonProperty("Seconds")]
        public int Seconds { get; set; }
        [JsonProperty("TotalDays")]
        public double TotalDays { get; set; }
        [JsonProperty("TotalHours")]
        public double TotalHours { get; set; }
        [JsonProperty("TotalMilliseconds")]
        public int TotalMilliseconds { get; set; }
        [JsonProperty("TotalMinutes")]
        public double TotalMinutes { get; set; }
        [JsonProperty("TotalSeconds")]
        public int TotalSeconds { get; set; }
    }

}
