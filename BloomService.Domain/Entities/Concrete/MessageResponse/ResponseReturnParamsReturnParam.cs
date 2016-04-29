namespace BloomService.Domain.Entities.Concrete.MessageResponse
{
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;

    [XmlType(AnonymousType = true)]
    public class ResponseReturnParamsReturnParam
    {
        [XmlArrayItem("Assignment", IsNullable = false)]
        public SageAssignment[] Assignments { get; set; }

        [XmlArrayItem("CallType", IsNullable = false)]
        public SageCallType[] CallTypes { get; set; }

        [XmlArrayItem("Department", IsNullable = false)]
        public SageDepartment[] Departments { get; set; }

        [XmlArrayItem("Employee", IsNullable = false)]
        public SageEmployee[] Employees { get; set; }

        [XmlArrayItem("Equipment", IsNullable = false)]
        public SageEquipment[] Equipments { get; set; }

        [XmlArrayItem("Location", IsNullable = false)]
        public SageLocation[] Locations { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Part", IsNullable = false)]
        public SagePart[] Parts { get; set; }

        [XmlArrayItem("Problem", IsNullable = false)]
        public SageProblem[] Problems { get; set; }

        [XmlArrayItem("Repair", IsNullable = false)]
        public SageRepair[] Repairs { get; set; }

        [XmlText]
        public string[] Text { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlArrayItem("WorkOrder", IsNullable = false)]
        public SageWorkOrder[] WorkOrders { get; set; }
    }
}