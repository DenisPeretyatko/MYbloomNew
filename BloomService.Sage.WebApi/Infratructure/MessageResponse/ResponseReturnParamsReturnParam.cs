namespace Sage.WebApi.Infratructure.MessageResponse
{
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.ReturnParamModels;

    [XmlType(AnonymousType = true)]
    public class ResponseReturnParamsReturnParam
    {
        [XmlArrayItem("Assignment", IsNullable = false)]
        public AssignmentReturnParam[] Assignments { get; set; }
        //public SageAssignment[] Assignments { get; set; }

        [XmlArrayItem("CallType", IsNullable = false)]
        public SageCallType[] CallTypes { get; set; }

        [XmlArrayItem("Department", IsNullable = false)]
        public SageDepartment[] Departments { get; set; }

        [XmlArrayItem("Employee", IsNullable = false)]
        public EmployeeReturnParam[] Employees { get; set; }
        //public SageEmployee[] Employees { get; set; }

        [XmlArrayItem("Equipment", IsNullable = false)]
        public EquipmentReturnParam[] Equipments { get; set; }
        //public SageEquipment[] Equipments { get; set; }

        [XmlArrayItem("Location", IsNullable = false)]
        public LocationReturnParam[] Locations { get; set; }
        //public SageLocation[] Locations { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Part", IsNullable = false)]
        public PartReturnParam[] Parts { get; set; }
        //public SagePart[] Parts { get; set; }

        [XmlArrayItem("Problem", IsNullable = false)]
        public SageProblem[] Problems { get; set; }

        [XmlArrayItem("Repair", IsNullable = false)]
        public SageRepair[] Repairs { get; set; }

        [XmlText]
        public string[] Text { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlArrayItem("WorkOrder", IsNullable = false)]
        public WorkOrderReturnParam[] WorkOrders { get; set; }
        //public SageWorkOrder[] WorkOrders { get; set; }
    }
}