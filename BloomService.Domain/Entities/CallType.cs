namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class SageCallType
    {
        /// <remarks/>
        [XmlAttribute]
        public string AgreementRequired { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string CallBack { get; set; }

        /// <remarks/>
        [XmlAttribute]
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