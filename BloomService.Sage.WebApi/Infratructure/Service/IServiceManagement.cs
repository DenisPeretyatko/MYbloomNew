namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface IServiceManagement
    {
        string CatalogPath { get; set; }

        SageAssignment[] AddAssignments(Properties properties);

        object Agreements();

        SageAssignment[] Assignments();

        SageAssignment[] Assignments(string number);

        SageCallType[] Calltypes();

        void Create(string name, string password);

        SageDepartment[] Departments();

        SageAssignment[] EditAssignments(Properties properties);

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

        SageWorkOrder[] WorkOrders(Properties properties);
    }
}