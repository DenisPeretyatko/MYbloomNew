﻿namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using Attributes;
    using System;
    [XmlType(AnonymousType = true)]
    [CollectionName("AssignmentCollection")]
    public class SageAssignment : SageEntity
    {
        [XmlAttribute]
        public string Alarm { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? AlarmDate { get; set; }

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

        [XmlAttribute(DataType = "time")]
        public TimeSpan? CreateTimeEntry { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateEntered { get; set; }

        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public double ElapsedTime { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? Enddate { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? Endtime { get; set; }

        [XmlAttribute]
        public string EnteredBy { get; set; }

        [XmlAttribute]
        public string EstimatedRepairHours { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? ETAdate { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? ETAtime { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? LastDate { get; set; }

        [XmlAttribute]
        public string LastStatus { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? LastTime { get; set; }

        [XmlAttribute]
        public string NextECardNumber { get; set; }

        [XmlAttribute]
        public string PaidLunchBreak { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? PostedTime { get; set; }

        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Problem { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? ScheduleDate { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? StartTime { get; set; }

        [XmlAttribute(DataType = "time")]
        public TimeSpan? TimeEntered { get; set; }

        [XmlAttribute]
        public string WorkOrder { get; set; }

        [XmlIgnore]
        public string Start { get; set; }
        [XmlIgnore]
        public string End { get; set; }
        [XmlIgnore]
        public string Location { get; set; }
        [XmlIgnore]
        public string Customer { get; set; }
        [XmlIgnore]
        public string Color { get; set; }
        [XmlIgnore]
        public string EmployeeId { get; set; }
    }
}