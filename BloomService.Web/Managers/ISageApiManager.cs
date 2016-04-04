namespace BloomService.Web.Managers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface ISageApiManager
    {
        string CatalogPath { get; set; }

        IEnumerable<SageAssignment> AddAssignments(Properties properties);

        object Agreements();

        IEnumerable<SageAssignment> Assignments();

        IEnumerable<SageAssignment> Assignments(string number);

        IEnumerable<SageCallType> Calltypes();

        void Create(string name, string password);

        IEnumerable<SageDepartment> Departments();

        IEnumerable<SageAssignment> EditAssignments(Properties properties);

        IEnumerable<SageEmployee> Employees();

        IEnumerable<SageEquipment> Equipments();

        IEnumerable<SageLocation> Locations();

        IEnumerable<SagePart> Parts();

        IEnumerable<string> PermissionCode();

        IEnumerable<SageProblem> Problems();

        IEnumerable<string> RateSheet();

        IEnumerable<SageRepair> Repairs();

        object SendMessage(string message);

        IEnumerable<SageWorkOrder> WorkOrders();

        IEnumerable<SageWorkOrder> WorkOrders(string number);

        IEnumerable<SageWorkOrder> WorkOrders(Properties properties);
    }
}