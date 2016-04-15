using BloomService.Domain.Enums;
using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class CallTypeModel
    {
        [JsonProperty("AgreementRequired")]
        public string AgreementRequired { get; set; }
        [JsonProperty("CallBack")]
        public string CallBack { get; set; }
        [JsonProperty("CallType")]
        public string CallType { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("FlatRateLaborProduct")]
        public string FlatRateLaborProduct { get; set; }
        [JsonProperty("FlatRatePartsProduct")]
        public string FlatRatePartsProduct { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("JobSaleProduct")]
        public string JobSaleProduct { get; set; }
        [JsonProperty("LaborProduct")]
        public string LaborProduct { get; set; }
        [JsonProperty("MiscellaneousProduct")]
        public string MiscellaneousProduct { get; set; }
        [JsonProperty("PartsProduct")]
        public string PartsProduct { get; set; }
        [JsonProperty("RateSheet")]
        public string RateSheet { get; set; }
        [JsonProperty("ServiceatCenter")]
        public string ServiceatCenter { get; set; }
        [JsonProperty("WorkOrderType")]
        public string WorkOrderType { get; set; }
    }
}