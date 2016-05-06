namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    using MongoDB.Bson.Serialization.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("CallTypeCollection")]
    public class SageCallType : SageEntity
    {
        [XmlAttribute]
        public string AgreementRequired { get; set; }

        [XmlAttribute]
        public string CallBack { get; set; }

        [XmlAttribute]
        public string CallType { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string FlatRateLaborProduct { get; set; }

        [XmlAttribute]
        public string FlatRatePartsProduct { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string JobSaleProduct { get; set; }

        [XmlAttribute]
        public string LaborProduct { get; set; }

        [XmlAttribute]
        public string MiscellaneousProduct { get; set; }

        [XmlAttribute]
        public string PartsProduct { get; set; }

        [XmlAttribute]
        public string RateSheet { get; set; }

        [XmlAttribute]
        public string ServiceatCenter { get; set; }

        [XmlAttribute]
        public string WorkOrderType { get; set; }
    }
}