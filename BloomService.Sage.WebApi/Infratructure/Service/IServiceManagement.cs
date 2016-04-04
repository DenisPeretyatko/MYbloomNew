namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface IServiceManagement
    {
        string CatalogPath { get; set; }
        void Create(string name, string password);
        object SendMessage(string message);
        SageLocation[] Locations();
        SagePart[] Parts();
        SageProblem[] Problems();
        SageEmployee[] Employees();
        SageWorkOrder[] WorkOrders();
        SageWorkOrder[] WorkOrders(string number);
        SageWorkOrder[] WorkOrders(Properties properties);
        SageRepair[] Repairs();
        SageCallType[] Calltypes();
        SageDepartment[] Departments();
        SageEquipment[] Equipments();
        SageAssignment[] Assignments();
        SageAssignment[] Assignments(string number);
        SageAssignment[] AddAssignments(Properties properties);
        SageAssignment[] EditAssignments(Properties properties);
        IEnumerable<string> RateSheet();
        IEnumerable<string> PermissionCode();
        object Agreements();
    }
}