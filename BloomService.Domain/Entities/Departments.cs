using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [CollectionNameAttribute("DepartmentCollection")]
    public partial class SageDepartment : SageEntity
    {
        private byte departmentField;

        private string descriptionField;

        private string inactiveField;

        private string gLPrefixField;

        private string gLSuffixField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [BsonId]
        public byte Department
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
        public string GLPrefix
        {
            get
            {
                return gLPrefixField;
            }
            set
            {
                gLPrefixField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GLSuffix
        {
            get
            {
                return gLSuffixField;
            }
            set
            {
                gLSuffixField = value;
            }
        }
    }


}