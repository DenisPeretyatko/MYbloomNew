namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageProblem : SageEntity
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
                return problemField;
            }
            set
            {
                problemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return descriptionField;
            }
            set
            {
                descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Department
        {
            get
            {
                return departmentField;
            }
            set
            {
                departmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedRepairHours
        {
            get
            {
                return estimatedRepairHoursField;
            }
            set
            {
                estimatedRepairHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Priority
        {
            get
            {
                return priorityField;
            }
            set
            {
                priorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Inactive
        {
            get
            {
                return inactiveField;
            }
            set
            {
                inactiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Skill
        {
            get
            {
                return skillField;
            }
            set
            {
                skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LaborRepair
        {
            get
            {
                return laborRepairField;
            }
            set
            {
                laborRepairField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCCostCode
        {
            get
            {
                return jCCostCodeField;
            }
            set
            {
                jCCostCodeField = value;
            }
        }
    }
}