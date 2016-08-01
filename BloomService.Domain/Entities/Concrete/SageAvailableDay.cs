namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;
    [XmlType(AnonymousType = true)]
    public class SageAvailableDay
    {
        [XmlAttribute]
        public long Id { get; set; }
        [XmlAttribute]
        public string ResourceId { get; set; }
        [XmlAttribute]
        public string End { get; set; }
        [XmlAttribute]
        public string Start { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
    }

}
