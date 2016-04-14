using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [CollectionNameAttribute("EmployeeCollection")]
    public partial class SageEmployee : SageEntity
    {

        private string employeeField;

        private string centerField;

        private string nameField;

        private string addressField;

        private string address2Field;

        private string cityField;

        private string stateField;

        private string zIPField;

        private string countryField;

        private string phoneField;

        private string faxField;

        private string pagerField;

        private string emailField;

        private string departmentField;

        private string stockLocationField;

        private string birthdateField;

        private string jobTitleField;

        private string statusField;

        private string inactiveField;

        private string pagerDeviceField;

        private string aliasField;

        private string jCJobField;

        private string jCCostCodeField;

        private string pREmployeeField;

        private string defaultStartTimeField;

        private string normalEndTimeField;

        private string workSaturdayField;

        private string workSundayField;

        private string defaultRepairField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [BsonId]
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
        public string Center
        {
            get
            {
                return centerField;
            }
            set
            {
                centerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address
        {
            get
            {
                return addressField;
            }
            set
            {
                addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address2
        {
            get
            {
                return address2Field;
            }
            set
            {
                address2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string City
        {
            get
            {
                return cityField;
            }
            set
            {
                cityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string State
        {
            get
            {
                return stateField;
            }
            set
            {
                stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ZIP
        {
            get
            {
                return zIPField;
            }
            set
            {
                zIPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Country
        {
            get
            {
                return countryField;
            }
            set
            {
                countryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Phone
        {
            get
            {
                return phoneField;
            }
            set
            {
                phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Fax
        {
            get
            {
                return faxField;
            }
            set
            {
                faxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Pager
        {
            get
            {
                return pagerField;
            }
            set
            {
                pagerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Email
        {
            get
            {
                return emailField;
            }
            set
            {
                emailField = value;
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
        public string StockLocation
        {
            get
            {
                return stockLocationField;
            }
            set
            {
                stockLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Birthdate
        {
            get
            {
                return birthdateField;
            }
            set
            {
                birthdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JobTitle
        {
            get
            {
                return jobTitleField;
            }
            set
            {
                jobTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
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
        public string PagerDevice
        {
            get
            {
                return pagerDeviceField;
            }
            set
            {
                pagerDeviceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Alias
        {
            get
            {
                return aliasField;
            }
            set
            {
                aliasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCJob
        {
            get
            {
                return jCJobField;
            }
            set
            {
                jCJobField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PREmployee
        {
            get
            {
                return pREmployeeField;
            }
            set
            {
                pREmployeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DefaultStartTime
        {
            get
            {
                return defaultStartTimeField;
            }
            set
            {
                defaultStartTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NormalEndTime
        {
            get
            {
                return normalEndTimeField;
            }
            set
            {
                normalEndTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkSaturday
        {
            get
            {
                return workSaturdayField;
            }
            set
            {
                workSaturdayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkSunday
        {
            get
            {
                return workSundayField;
            }
            set
            {
                workSundayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DefaultRepair
        {
            get
            {
                return defaultRepairField;
            }
            set
            {
                defaultRepairField = value;
            }
        }
    }
}