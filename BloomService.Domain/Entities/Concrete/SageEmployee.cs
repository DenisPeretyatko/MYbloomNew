namespace BloomService.Domain.Entities.Concrete
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("EmployeeCollection")]
    public class SageEmployee : SageEntity
    {
        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string Address2 { get; set; }

        [XmlAttribute]
        public string Alias { get; set; }

        [XmlAttribute]
        public string Birthdate { get; set; }

        [XmlAttribute]
        public string Center { get; set; }

        [XmlAttribute]
        public string City { get; set; }

        [XmlAttribute]
        public string Country { get; set; }

        [XmlAttribute]
        public string DefaultRepair { get; set; }

        [XmlAttribute]
        public string DefaultStartTime { get; set; }

        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Email { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute]
        public string Fax { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string JCCostCode { get; set; }

        [XmlAttribute]
        public string JCJob { get; set; }

        [XmlAttribute]
        public string JobTitle { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string NormalEndTime { get; set; }

        [XmlAttribute]
        public string Pager { get; set; }

        [XmlAttribute]
        public string PagerDevice { get; set; }

        [XmlAttribute]
        public string Phone { get; set; }

        [XmlAttribute]
        public string PREmployee { get; set; }

        [XmlAttribute]
        public string State { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string StockLocation { get; set; }

        [XmlAttribute]
        public string WorkSaturday { get; set; }

        [XmlAttribute]
        public string WorkSunday { get; set; }

        [XmlAttribute]
        public string ZIP { get; set; }


        [XmlAttribute]
        public List<SageAvailableDay> AvailableDays { get; set; }
        [XmlAttribute]
        public bool IsAvailable { get; set; }
        [XmlAttribute]
        public string Picture { get; set; }
    }
}