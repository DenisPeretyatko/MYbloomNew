namespace BloomService.Domain.Entities.Concrete
{
    using System;
    using System.Xml.Serialization;

    using Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("TechnicianLocationCollection")]
    public class SageTechnicianLocation : SageEntity
    {
        [XmlAttribute]
        public long Employee { get; set; }

        [XmlAttribute(DataType = "dateTime")]
        public DateTime Date { get; set; }
        
        [XmlAttribute]
        public decimal Longitude { get; set; }

        [XmlAttribute]
        public decimal Latitude { get; set; }
    }
}