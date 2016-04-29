namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    using MongoDB.Bson.Serialization.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("RepairCollection")]
    public class SageRepair : SageEntity
    {
        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string JCCostCode { get; set; }

        [XmlAttribute]
        [BsonId]
        public byte Repair { get; set; }
    }
}