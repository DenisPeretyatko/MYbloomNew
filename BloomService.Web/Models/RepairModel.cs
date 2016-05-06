using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class RepairModel : EntityModel
    {
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("JCCostCode")]
        public string JcCostCode { get; set; }
        [JsonProperty("Repair")]
        public byte Repair { get; set; }
    }
}