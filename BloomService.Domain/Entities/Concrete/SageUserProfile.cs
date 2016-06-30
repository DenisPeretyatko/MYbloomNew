using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BloomService.Domain.Attributes;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("NotificationTimeCollection")]
    public class SageUserProfile : SageEntity
    {
        [XmlAttribute]
        public string UserId { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
        [XmlAttribute(DataType = "time")]
        public TimeSpan Time { get; set; }
    }
}
