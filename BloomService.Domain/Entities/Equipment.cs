using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [CollectionNameAttribute("EquipmentCollection")]
    public partial class SageEquipment : SageEntity
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
        [BsonId]
        public string Equipment
        {
            get
            {
                return equipmentField;
            }
            set
            {
                equipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Parentnumber
        {
            get
            {
                return parentnumberField;
            }
            set
            {
                parentnumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Part
        {
            get
            {
                return partField;
            }
            set
            {
                partField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerialNumber
        {
            get
            {
                return serialNumberField;
            }
            set
            {
                serialNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EquipmentType
        {
            get
            {
                return equipmentTypeField;
            }
            set
            {
                equipmentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Model
        {
            get
            {
                return modelField;
            }
            set
            {
                modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Manufacturer
        {
            get
            {
                return manufacturerField;
            }
            set
            {
                manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string YearofManufacturing
        {
            get
            {
                return yearofManufacturingField;
            }
            set
            {
                yearofManufacturingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InstallDate
        {
            get
            {
                return installDateField;
            }
            set
            {
                installDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InstallLocation
        {
            get
            {
                return installLocationField;
            }
            set
            {
                installLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateReplaced
        {
            get
            {
                return dateReplacedField;
            }
            set
            {
                dateReplacedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateRemoved
        {
            get
            {
                return dateRemovedField;
            }
            set
            {
                dateRemovedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyStarts
        {
            get
            {
                return warrantyStartsField;
            }
            set
            {
                warrantyStartsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyExpires
        {
            get
            {
                return warrantyExpiresField;
            }
            set
            {
                warrantyExpiresField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Employee
        {
            get
            {
                return employeeField;
            }
            set
            {
                employeeField = value;
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
        public string Location
        {
            get
            {
                return locationField;
            }
            set
            {
                locationField = value;
            }
        }
    }
}