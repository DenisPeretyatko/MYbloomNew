namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageEquipment
    {

        private string equipmentField;

        private string parentnumberField;

        private string partField;

        private string serialNumberField;

        private string equipmentTypeField;

        private string modelField;

        private string manufacturerField;

        private string yearofManufacturingField;

        private string installDateField;

        private string installLocationField;

        private string dateReplacedField;

        private string dateRemovedField;

        private string warrantyStartsField;

        private string warrantyExpiresField;

        private string employeeField;

        private string inactiveField;

        private string locationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Equipment
        {
            get
            {
                return this.equipmentField;
            }
            set
            {
                this.equipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Parentnumber
        {
            get
            {
                return this.parentnumberField;
            }
            set
            {
                this.parentnumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Part
        {
            get
            {
                return this.partField;
            }
            set
            {
                this.partField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerialNumber
        {
            get
            {
                return this.serialNumberField;
            }
            set
            {
                this.serialNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EquipmentType
        {
            get
            {
                return this.equipmentTypeField;
            }
            set
            {
                this.equipmentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string YearofManufacturing
        {
            get
            {
                return this.yearofManufacturingField;
            }
            set
            {
                this.yearofManufacturingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InstallDate
        {
            get
            {
                return this.installDateField;
            }
            set
            {
                this.installDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InstallLocation
        {
            get
            {
                return this.installLocationField;
            }
            set
            {
                this.installLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateReplaced
        {
            get
            {
                return this.dateReplacedField;
            }
            set
            {
                this.dateReplacedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateRemoved
        {
            get
            {
                return this.dateRemovedField;
            }
            set
            {
                this.dateRemovedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyStarts
        {
            get
            {
                return this.warrantyStartsField;
            }
            set
            {
                this.warrantyStartsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyExpires
        {
            get
            {
                return this.warrantyExpiresField;
            }
            set
            {
                this.warrantyExpiresField = value;
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
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
    }
}