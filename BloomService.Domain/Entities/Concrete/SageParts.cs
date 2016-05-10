namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("PartCollection")]
    public class SagePart : SageEntity
    {
        [XmlAttribute]
        public string AgreementPrice { get; set; }

        [XmlAttribute]
        public string AKA { get; set; }

        [XmlAttribute]
        public string AllowFractions { get; set; }

        [XmlAttribute("Auto-GenerateEquipment")]
        public string AutoGenerateEquipment { get; set; }

        [XmlAttribute]
        public string AverageCost { get; set; }

        [XmlAttribute]
        public string BadInventoryValue { get; set; }

        [XmlAttribute]
        public string BinLocation { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string EquipmentType { get; set; }

        [XmlAttribute]
        public string ExchangeCost { get; set; }

        [XmlAttribute]
        public string ExchangePrice { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string InStock { get; set; }

        [XmlAttribute]
        public string LastActivityDate { get; set; }

        [XmlAttribute]
        public string LastOrderDate { get; set; }

        [XmlAttribute]
        public string LastPrice { get; set; }

        [XmlAttribute]
        public string LastReceivedDate { get; set; }

        [XmlAttribute]
        public string Level1Price { get; set; }

        [XmlAttribute]
        public string Level2Price { get; set; }

        [XmlAttribute]
        public string Level3Price { get; set; }

        [XmlAttribute]
        public string Lotted { get; set; }

        [XmlAttribute]
        public string Manufacturer { get; set; }

        [XmlAttribute]
        public string ManufacturerNumber { get; set; }

        [XmlAttribute]
        public string MinimumStockQuantity { get; set; }

        [XmlAttribute]
        public string Model { get; set; }

        [XmlAttribute]
        public string OrderLeadTime { get; set; }

        [XmlAttribute]
        public string Part { get; set; }

        [XmlAttribute]
        public string PartNumber { get; set; }

        [XmlAttribute]
        public string Product { get; set; }

        [XmlAttribute]
        public string QuantityAllocated { get; set; }

        [XmlAttribute]
        public string QuantityonHand { get; set; }

        [XmlAttribute]
        public string QuantityonOrder { get; set; }

        [XmlAttribute]
        public string ReimbursementAmount { get; set; }

        [XmlAttribute]
        public string ReorderQuantity { get; set; }

        [XmlAttribute]
        public string Serialized { get; set; }

        [XmlAttribute]
        public string StandardCost { get; set; }

        [XmlAttribute]
        public string TotalQuantity { get; set; }

        [XmlAttribute]
        public string Unit { get; set; }

        [XmlAttribute]
        public string UsedCost { get; set; }

        [XmlAttribute]
        public string UsedPrice { get; set; }

        [XmlAttribute]
        public string Vendor { get; set; }

        [XmlAttribute]
        public string WarningLevel { get; set; }

        [XmlAttribute]
        public string WarrantyMonths { get; set; }
    }
}