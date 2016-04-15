﻿namespace BloomService.Domain.Entities
{
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;

    using MongoDB.Bson.Serialization.Attributes;

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
        [BsonId]
        public byte Problem { get; set; }

        [XmlAttribute]
        public string Skill { get; set; }
    }
}