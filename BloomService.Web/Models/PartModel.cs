using Newtonsoft.Json;
using System;

namespace BloomService.Web.Models
{  
    public class PartModel : EntityModel
    {
        [JsonProperty("AgreementPrice")]
        public string AgreementPrice { get; set; }

        [JsonProperty("AKA")]
        public string AKA { get; set; }

        [JsonProperty("AllowFractions")]
        public string AllowFractions { get; set; }

        [JsonProperty("AutoGenerateEquipment")]
        public string AutoGenerateEquipment { get; set; }

        [JsonProperty("AverageCost")]
        public string AverageCost { get; set; }

        [JsonProperty("BadInventoryValue")]
        public string BadInventoryValue { get; set; }

        [JsonProperty("BinLocation")]
        public string BinLocation { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("EquipmentType")]
        public string EquipmentType { get; set; }

        [JsonProperty("ExchangeCost")]
        public string ExchangeCost { get; set; }

        [JsonProperty("ExchangePrice")]
        public string ExchangePrice { get; set; }

        [JsonProperty("Inactive")]
        public string Inactive { get; set; }

        [JsonProperty("InStock")]
        public string InStock { get; set; }

        [JsonProperty("LastActivityDate")]
        public DateTime? LastActivityDate { get; set; }

        [JsonProperty("LastOrderDate")]
        public DateTime? LastOrderDate { get; set; }

        [JsonProperty("LastPrice")]
        public string LastPrice { get; set; }

        [JsonProperty("LastReceivedDate")]
        public DateTime? LastReceivedDate { get; set; }

        [JsonProperty("Level1Price")]
        public string Level1Price { get; set; }

        [JsonProperty("Level2Price")]
        public string Level2Price { get; set; }

        [JsonProperty("Level3Price")]
        public string Level3Price { get; set; }

        [JsonProperty("Lotted")]
        public string Lotted { get; set; }

        [JsonProperty("Manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("ManufacturerNumber")]
        public string ManufacturerNumber { get; set; }

        [JsonProperty("MinimumStockQuantity")]
        public string MinimumStockQuantity { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("OrderLeadTime")]
        public DateTime? OrderLeadTime { get; set; }

        [JsonProperty("Part")]
        public string Part { get; set; }

        [JsonProperty("PartNumber")]
        public string PartNumber { get; set; }

        [JsonProperty("Product")]
        public string Product { get; set; }

        [JsonProperty("QuantityAllocated")]
        public string QuantityAllocated { get; set; }

        [JsonProperty("QuantityonHand")]
        public string QuantityonHand { get; set; }

        [JsonProperty("QuantityonOrder")]
        public string QuantityonOrder { get; set; }

        [JsonProperty("ReimbursementAmount")]
        public string ReimbursementAmount { get; set; }

        [JsonProperty("ReorderQuantity")]
        public string ReorderQuantity { get; set; }

        [JsonProperty("Serialized")]
        public string Serialized { get; set; }

        [JsonProperty("StandardCost")]
        public string StandardCost { get; set; }

        [JsonProperty("TotalQuantity")]
        public string TotalQuantity { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("UsedCost")]
        public string UsedCost { get; set; }

        [JsonProperty("UsedPrice")]
        public string UsedPrice { get; set; }

        [JsonProperty("Vendor")]
        public string Vendor { get; set; }

        [JsonProperty("WarningLevel")]
        public string WarningLevel { get; set; }

        [JsonProperty("WarrantyMonths")]
        public string WarrantyMonths { get; set; }
    }
}