namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("AssignmentCollection")]
    public class SageAssignment : SageEntity
    {
        [XmlAttribute]
        public string Alarm { get; set; }

        [XmlAttribute]
        public string AlarmDate { get; set; }

        [XmlAttribute]
        public string AlarmStatus { get; set; }

        [XmlAttribute]
        public string Area { get; set; }

        [XmlAttribute]
        public string Assignment { get; set; }

        [XmlAttribute]
        public string Assignmenttype { get; set; }

        [XmlAttribute]
        public string CallType { get; set; }

        [XmlAttribute]
        public string Center { get; set; }

        [XmlAttribute]
        public string Comments { get; set; }

        [XmlAttribute]
        public string CreateTimeEntry { get; set; }

        [XmlAttribute]
        public string DateEntered { get; set; }

        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string ElapsedTime { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute]
        public string Enddate { get; set; }

        [XmlAttribute]
        public string Endtime { get; set; }

        [XmlAttribute]
        public string EnteredBy { get; set; }

        [XmlAttribute]
        public string EstimatedRepairHours { get; set; }

        [XmlAttribute]
        public string ETAdate { get; set; }

        [XmlAttribute]
        public string ETAtime { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string LastDate { get; set; }

        [XmlAttribute]
        public string LastStatus { get; set; }

        [XmlAttribute]
        public string LastTime { get; set; }

        [XmlAttribute]
        public string NextECardNumber { get; set; }

        [XmlAttribute]
        public string PaidLunchBreak { get; set; }

        [XmlAttribute]
        public string PostedTime { get; set; }

        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Problem { get; set; }

        [XmlAttribute]
        public string ScheduleDate { get; set; }

        [XmlAttribute]
        public string StartTime { get; set; }

        [XmlAttribute]
        public string TimeEntered { get; set; }

        [XmlAttribute]
        public string WorkOrder { get; set; }
    }
}