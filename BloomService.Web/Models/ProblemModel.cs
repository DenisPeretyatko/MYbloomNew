using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class ProblemModel
    {
        [JsonProperty("Problem")]
        public int Problem { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Department")]
        public string Department { get; set; }
        [JsonProperty("EstimatedRepairHours")]
        public int EstimatedRepairHours { get; set; }
        [JsonProperty("Priority")]
        public string Priority { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("Skill")]
        public string Skill { get; set; }
        [JsonProperty("LaborRepair")]
        public string LaborRepair { get; set; }
        [JsonProperty("JCCostCode")]
        public string JcCostCode { get; set; }
    }
}