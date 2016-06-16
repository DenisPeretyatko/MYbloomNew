namespace BloomService.Domain.Entities.Concrete
{
    using System;
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    [XmlType(AnonymousType = true)]
    [CollectionName("ChangeCollection")]
    public class SageChange : SageEntity
    {
        [XmlAttribute]
        public ChangeType Change { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? ChangeTime { get; set; }

        [XmlAttribute]
        public string EntityId { get; set; }

        [XmlAttribute]
        public string EntityType { get; set; }

        [XmlAttribute]
        public StatusType Status { get; set; }
    }
}