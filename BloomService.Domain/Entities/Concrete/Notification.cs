using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BloomService.Domain.Attributes;
using BloomService.Domain.Entities.Abstract;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("NotificationCollection")]
    public class Notification : SageEntity
    {
        [XmlAttribute]
        public bool IsViewed { get; set; }
        [XmlAttribute]
        public string Message { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
        [XmlAttribute(DataType = "time")]
        public TimeSpan Time { get; set; }
    }
}
