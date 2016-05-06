namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    using MongoDB.Bson.Serialization.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("DepartmentCollection")]
    public class SageDepartment : SageEntity
    {
        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string GLPrefix { get; set; }

        [XmlAttribute]
        public string GLSuffix { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }
    }
}