using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class EmployeeModel : EntityModel
    {
        [JsonProperty("Employee")]
        public long Employee { get; set; }
        [JsonProperty("Center")]
        public string Center { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("Address2")]
        public string Address2 { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("ZIP")]
        public string Zip { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
        [JsonProperty("Fax")]
        public string Fax { get; set; }
        [JsonProperty("Pager")]
        public string Pager { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Department")]
        public string Department { get; set; }
        [JsonProperty("StockLocation")]
        public string StockLocation { get; set; }
        [JsonProperty("Birthdate")]
        public DateTime Birthdate { get; set; }
        [JsonProperty("JobTitle")]
        public string JobTitle { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("PagerDevice")]
        public string PagerDevice { get; set; }
        [JsonProperty("Alias")]
        public string Alias { get; set; }
        [JsonProperty("JCJob")]
        public string JcJob { get; set; }
        [JsonProperty("JCCostCode")]
        public string JcCostCode { get; set; }
        [JsonProperty("PREmployee")]
        public string PrEmployee { get; set; }
        [JsonProperty("DefaultStartTime")]
        public DateTime DefaultStartTime { get; set; }
        [JsonProperty("NormalEndTime")]
        public DateTime NormalEndTime { get; set; }
        [JsonProperty("WorkSaturday")]
        public string WorkSaturday { get; set; }
        [JsonProperty("WorkSunday")]
        public string WorkSunday { get; set; }
        [JsonProperty("DefaultRepair")]
        public string DefaultRepair { get; set; }


        [JsonProperty("AvailableDays")]
        public List<AvailableDay> AvailableDays { get; set; }
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }
        [JsonProperty("Picture")]
        public string Picture { get; set; }
        [JsonProperty("Color")]
        public string Color { get; set; }
    }
}