namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageProblem
    {

        private byte problemField;

        private string descriptionField;

        private string departmentField;

        private decimal estimatedRepairHoursField;

        private string priorityField;

        private string inactiveField;

        private string skillField;

        private string laborRepairField;

        private string jCCostCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Problem
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
        public decimal EstimatedRepairHours
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
        public string Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LaborRepair
        {
            get
            {
                return this.laborRepairField;
            }
            set
            {
                this.laborRepairField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCCostCode
        {
            get
            {
                return this.jCCostCodeField;
            }
            set
            {
                this.jCCostCodeField = value;
            }
        }
    }
}