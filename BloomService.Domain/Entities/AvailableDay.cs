using System.Xml.Serialization;

namespace BloomService.Domain.Entities
{
    using Newtonsoft.Json;

    public class AvailableDay
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
