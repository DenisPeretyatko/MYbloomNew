using System;
using System.Xml.Serialization;
using BloomService.Domain.Attributes;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("NotificationTimeCollection")]
    public class SageUserProfile : SageEntity
    {
        [XmlAttribute]
        public long UserId { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
        [XmlAttribute(DataType = "time")]
        public TimeSpan Time { get; set; }
    }
}
