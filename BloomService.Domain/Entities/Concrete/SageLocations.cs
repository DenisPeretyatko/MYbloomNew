namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("LocationCollection")]
    public class SageLocation : SageEntity
    {
        [XmlAttribute]
        public string ABN { get; set; }

        [XmlAttribute]
        public string AccountOpenDate { get; set; }

        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string Address2 { get; set; }

        [XmlAttribute]
        public string Alias { get; set; }

        [XmlAttribute]
        public string ARCustomer { get; set; }

        [XmlAttribute]
        public string Area { get; set; }

        [XmlAttribute("Bill-outAuthorization")]
        public string BilloutAuthorization { get; set; }

        [XmlAttribute]
        public string Center { get; set; }

        [XmlAttribute]
        public string City { get; set; }

        [XmlAttribute]
        public string Contact1Email { get; set; }

        [XmlAttribute]
        public string Contact1Fax { get; set; }

        [XmlAttribute]
        public string Contact1Mobile { get; set; }

        [XmlAttribute]
        public string Contact1Name { get; set; }

        [XmlAttribute]
        public string Contact1Phone { get; set; }

        [XmlAttribute]
        public string Contact1Title { get; set; }

        [XmlAttribute]
        public string Contact2Email { get; set; }

        [XmlAttribute]
        public string Contact2Fax { get; set; }

        [XmlAttribute]
        public string Contact2Mobile { get; set; }

        [XmlAttribute]
        public string Contact2Name { get; set; }

        [XmlAttribute]
        public string Contact2Phone { get; set; }

        [XmlAttribute]
        public string Contact2Title { get; set; }

        [XmlAttribute]
        public string Country { get; set; }

        [XmlAttribute]
        public string CustomerType { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute]
        public string ExemptStatus { get; set; }

        [XmlAttribute]
        public string GLPrefix { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string JCExtra { get; set; }

        [XmlAttribute]
        public string JCJob { get; set; }

        [XmlAttribute]
        public string Location { get; set; }

        [XmlAttribute]
        public string LocationKey { get; set; }

        [XmlAttribute]
        public string MainFax { get; set; }

        [XmlAttribute]
        public string MainPhone { get; set; }

        [XmlAttribute]
        public string MapLocation { get; set; }

        [XmlAttribute]
        public string Memo { get; set; }

        [XmlAttribute]
        public string Miscellaneous { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string PayTerms { get; set; }

        [XmlAttribute]
        public string PermissionCode { get; set; }

        [XmlAttribute]
        public string PREmployee { get; set; }

        [XmlAttribute]
        public string RateSheet { get; set; }

        [XmlAttribute]
        public string SalesEmployee { get; set; }

        [XmlAttribute]
        public string SiteType { get; set; }

        [XmlAttribute]
        public string State { get; set; }

        [XmlAttribute]
        public string TaxGroup { get; set; }

        [XmlAttribute]
        public string ZIP { get; set; }
    }
}