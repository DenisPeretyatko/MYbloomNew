namespace BloomService.Web.Managers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface ISageApiManager
    {
        IEnumerable<SageAssignment> AddAssignments(Properties properties);

        SageAssignment Assignments(string id);

        IEnumerable<SageAssignment> Assignments();

        IEnumerable<SageCallType> Calltypes();

        IEnumerable<SageDepartment> Departments();

        IEnumerable<SageAssignment> EditAssignments(Properties properties);

        IEnumerable<SageEmployee> Employees();

        IEnumerable<SageEquipment> Equipment();

        IEnumerable<SageLocation> Locations();

        IEnumerable<SagePart> Parts();

        IEnumerable<string> PermissionCode();

        IEnumerable<SageProblem> Problems();

        IEnumerable<string> RateSheet();

        IEnumerable<SageRepair> Repairs();

        SageWorkOrder Workorders(string id);

        IEnumerable<SageWorkOrder> Workorders();

        IEnumerable<SageWorkOrder> Workorders(Properties properties);
    }
}