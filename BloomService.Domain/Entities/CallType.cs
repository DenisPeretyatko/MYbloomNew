namespace BloomService.Domain.Entities
{
    using Attributes;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [CollectionNameAttribute("CallTypeCollection")]
    public class SageCallType : SageEntity
    {
        /// <remarks/>
        [XmlAttribute]
        public string AgreementRequired { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string CallBack { get; set; }

        /// <remarks/>
        [XmlAttribute]
        [BsonId]
        public string CallType { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Description { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string FlatRateLaborProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string FlatRatePartsProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Inactive { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string JobSaleProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string LaborProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string MiscellaneousProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string PartsProduct { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string RateSheet { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string ServiceatCenter { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string WorkOrderType { get; set; }
    }
}