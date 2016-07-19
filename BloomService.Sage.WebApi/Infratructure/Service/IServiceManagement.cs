namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;

    public interface IServiceManagement
    {
        string SendMessageXml(string message);

        string CatalogPath { get; set; }

        SageAssignment[] AddAssignments(Dictionary<string, string> properties);

        object Agreements();

        SageAssignment[] Assignments();

        SageAssignment[] Assignments(string number);

        SageCallType[] Calltypes();

        void Create(string name, string password);

        SageDepartment[] Departments();

        SageAssignment[] EditAssignments(Dictionary<string, string> properties);

        SageEmployee[] Employees();

        SageEquipment[] Equipments();

        SageLocation[] Locations();

        SagePart[] Parts();

        IEnumerable<string> PermissionCode();

        SageProblem[] Problems();

        IEnumerable<string> RateSheet();

        SageRepair[] Repairs();

        object SendMessage(string message);

        SageWorkOrder[] WorkOrders();

        SageWorkOrder[] WorkOrders(string number);

        SageWorkOrder[] WorkOrders(Dictionary<string, string> properties);

        bool AddWorkOrderItem(Dictionary<string, string> properties);

        SageEquipment[] GetEquipmentsByWorkOrderId(string id);

        void UnassignWorkOrder(string id);

        SageAssignment[] GetAssignmentByWorkOrderId(string number);
    }
}