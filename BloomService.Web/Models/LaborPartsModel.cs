using Newtonsoft.Json;
using System;

namespace BloomService.Web.Models
{
    public class LaborPartsModel
    {
        public DateTime WorkDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public float CostQty { get; set; }
        public float BiledQty { get; set; }
        public float Rate { get; set; }
    }
}