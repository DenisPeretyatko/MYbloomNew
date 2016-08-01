namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("DepartmentCollection")]
    public class SageDepartment : SageEntity
    {
        [XmlAttribute]
        public long Department { get; set; }

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