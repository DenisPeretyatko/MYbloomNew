namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    public class TimeStamp
    {
        [XmlAttribute]
        public long Ticks { get; set; }
        [XmlAttribute]
        public int Days { get; set; }
        [XmlAttribute]
        public int Hours { get; set; }
        [XmlAttribute]
        public int Milliseconds { get; set; }
        [XmlAttribute]
        public int Minutes { get; set; }
        [XmlAttribute]
        public int Seconds { get; set; }
        [XmlAttribute]
        public double TotalDays { get; set; }
        [XmlAttribute]
        public double TotalHours { get; set; }
        [XmlAttribute]
        public int TotalMilliseconds { get; set; }
        [XmlAttribute]
        public double TotalMinutes { get; set; }
        [XmlAttribute]
        public int TotalSeconds { get; set; }
    }

}
