namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;
    using System;
    [XmlType(AnonymousType = true)]
    [CollectionName("EquipmentCollection")]
    public class SageEquipment : SageEntity
    {
        [XmlAttribute(DataType = "date")]
        public DateTime? DateRemoved { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateReplaced { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute]
        public long Equipment { get; set; }

        [XmlAttribute]
        public string EquipmentType { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? InstallDate { get; set; }

        [XmlAttribute]
        public string InstallLocation { get; set; }

        [XmlAttribute]
        public string Location { get; set; }

        [XmlAttribute]
        public string Manufacturer { get; set; }

        [XmlAttribute]
        public string Model { get; set; }

        [XmlAttribute]
        public string Parentnumber { get; set; }

        [XmlAttribute]
        public string Part { get; set; }

        [XmlAttribute]
        public string SerialNumber { get; set; }

        [XmlAttribute]
        public string WarrantyExpires { get; set; }

        [XmlAttribute]
        public string WarrantyStarts { get; set; }

        [XmlAttribute]
        public string YearofManufacturing { get; set; }
    }
}