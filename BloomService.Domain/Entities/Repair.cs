using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [CollectionNameAttribute("RepairCollection")]
    public partial class SageRepair : SageEntity
    {

        private byte repairField;

        private string descriptionField;

        private string inactiveField;

        private string jCCostCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [BsonId]
        public byte Repair
        {
            get
            {
                return repairField;
            }
            set
            {
                repairField = value;
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