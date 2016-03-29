using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sage.WebApi.Models.SerializeModels
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageAssignment
    {
        private string scheduleDateField;

        private string employeeField;

        private string workOrderField;

        private string estimatedRepairHoursField;

        private string callTypeField;

        private string areaField;

        private string priorityField;

        private string startTimeField;

        private string problemField;

        private string elapsedTimeField;

        private string centerField;

        private string lastTimeField;

        private string lastStatusField;

        private string alarmStatusField;

        private string alarmDateField;

        private string postedTimeField;

        private string assignmentField;

        private string inactiveField;

        private string alarmField;

        private string dateEnteredField;

        private string timeEnteredField;

        private string enteredByField;

        private string lastDateField;

        private string createTimeEntryField;

        private string commentsField;

        private string enddateField;

        private string endtimeField;

        private string assignmenttypeField;

        private string nextECardNumberField;

        private string paidLunchBreakField;

        private string departmentField;

        private string descriptionField;

        private string eTAdateField;

        private string eTAtimeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ScheduleDate
        {
            get
            {
                return this.scheduleDateField;
            }
            set
            {
                this.scheduleDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Employee
        {
            get
            {
                return this.employeeField;
            }
            set
            {
                this.employeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkOrder
        {
            get
            {
                return this.workOrderField;
            }
            set
            {
                this.workOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EstimatedRepairHours
        {
            get
            {
                return this.estimatedRepairHoursField;
            }
            set
            {
                this.estimatedRepairHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CallType
        {
            get
            {
                return this.callTypeField;
            }
            set
            {
                this.callTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Area
        {
            get
            {
                return this.areaField;
            }
            set
            {
                this.areaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Problem
        {
            get
            {
                return this.problemField;
            }
            set
            {
                this.problemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ElapsedTime
        {
            get
            {
                return this.elapsedTimeField;
            }
            set
            {
                this.elapsedTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Center
        {
            get
            {
                return this.centerField;
            }
            set
            {
                this.centerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastTime
        {
            get
            {
                return this.lastTimeField;
            }
            set
            {
                this.lastTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastStatus
        {
            get
            {
                return this.lastStatusField;
            }
            set
            {
                this.lastStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlarmStatus
        {
            get
            {
                return this.alarmStatusField;
            }
            set
            {
                this.alarmStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlarmDate
        {
            get
            {
                return this.alarmDateField;
            }
            set
            {
                this.alarmDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PostedTime
        {
            get
            {
                return this.postedTimeField;
            }
            set
            {
                this.postedTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Assignment
        {
            get
            {
                return this.assignmentField;
            }
            set
            {
                this.assignmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Inactive
        {
            get
            {
                return this.inactiveField;
            }
            set
            {
                this.inactiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Alarm
        {
            get
            {
                return this.alarmField;
            }
            set
            {
                this.alarmField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateEntered
        {
            get
            {
                return this.dateEnteredField;
            }
            set
            {
                this.dateEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TimeEntered
        {
            get
            {
                return this.timeEnteredField;
            }
            set
            {
                this.timeEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EnteredBy
        {
            get
            {
                return this.enteredByField;
            }
            set
            {
                this.enteredByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastDate
        {
            get
            {
                return this.lastDateField;
            }
            set
            {
                this.lastDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CreateTimeEntry
        {
            get
            {
                return this.createTimeEntryField;
            }
            set
            {
                this.createTimeEntryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Enddate
        {
            get
            {
                return this.enddateField;
            }
            set
            {
                this.enddateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Endtime
        {
            get
            {
                return this.endtimeField;
            }
            set
            {
                this.endtimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Assignmenttype
        {
            get
            {
                return this.assignmenttypeField;
            }
            set
            {
                this.assignmenttypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NextECardNumber
        {
            get
            {
                return this.nextECardNumberField;
            }
            set
            {
                this.nextECardNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PaidLunchBreak
        {
            get
            {
                return this.paidLunchBreakField;
            }
            set
            {
                this.paidLunchBreakField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Department
        {
            get
            {
                return this.departmentField;
            }
            set
            {
                this.departmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ETAdate
        {
            get
            {
                return this.eTAdateField;
            }
            set
            {
                this.eTAdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ETAtime
        {
            get
            {
                return this.eTAtimeField;
            }
            set
            {
                this.eTAtimeField = value;
            }
        }
    }

}