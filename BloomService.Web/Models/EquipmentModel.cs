using System;
using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class EquipmentModel : EntityModel
    {
        [JsonProperty("Equipment")]
        public string Equipment { get; set; }
        [JsonProperty("Parentnumber")]
        public string Parentnumber { get; set; }
        [JsonProperty("Part")]
        public string Part { get; set; }
        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }
        [JsonProperty("EquipmentType")]
        public string EquipmentType { get; set; }
        [JsonProperty("Model")]
        public string Model { get; set; }
        [JsonProperty("Manufacturer")]
        public string Manufacturer { get; set; }
        [JsonProperty("YearofManufacturing")]
        public string YearofManufacturing { get; set; }
        [JsonProperty("InstallDate")]
        public DateTime InstallDate { get; set; }
        [JsonProperty("InstallLocation")]
        public string InstallLocation { get; set; }
        [JsonProperty("DateReplaced")]
        public DateTime DateReplaced { get; set; }
        [JsonProperty("DateRemoved")]
        public DateTime DateRemoved { get; set; }
        [JsonProperty("WarrantyStarts")]
        public string WarrantyStarts { get; set; }
        [JsonProperty("WarrantyExpires")]
        public string WarrantyExpires { get; set; }
        [JsonProperty("Employee")]
        public string Employee { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("Location")]
        public string Location { get; set; }
    }
}