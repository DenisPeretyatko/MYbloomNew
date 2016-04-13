namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class SageAssignment : SageEntity
    {
        /// <remarks/>
        [XmlAttribute]
        public string Alarm { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string AlarmDate { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string AlarmStatus { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Area { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Assignment { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Assignmenttype { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string CallType { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Center { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Comments { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string CreateTimeEntry { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string DateEntered { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Department { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Description { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string ElapsedTime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Employee { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Enddate { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Endtime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string EnteredBy { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string EstimatedRepairHours { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string ETAdate { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string ETAtime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Inactive { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string LastDate { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string LastStatus { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string LastTime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string NextECardNumber { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string PaidLunchBreak { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string PostedTime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Priority { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Problem { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string ScheduleDate { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string StartTime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string TimeEntered { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string WorkOrder { get; set; }
    }
}