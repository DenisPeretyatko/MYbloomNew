namespace BloomService.Domain.Entities.Concrete
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    [XmlType(AnonymousType = true)]
    [CollectionName("ProblemCollection")]
    public class SageProblem : SageEntity
    {
        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public decimal EstimatedRepairHours { get; set; }

        [XmlAttribute]
        public string Inactive { get; set; }

        [XmlAttribute]
        public string JCCostCode { get; set; }

        [XmlAttribute]
        public string LaborRepair { get; set; }

        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Problem { get; set; }

        [XmlAttribute]
        public string Skill { get; set; }
    }
}