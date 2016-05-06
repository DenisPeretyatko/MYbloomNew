namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    public class SageAvailableDay
    {
        [XmlAttribute]
        public string Id { get; set; }
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
