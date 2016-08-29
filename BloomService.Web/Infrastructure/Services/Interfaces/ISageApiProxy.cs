namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using System.Collections.Generic;
    public interface ISageApiProxy
    {
        SageResponse<SageCustomer> GetCustomers();
        SageResponse<SageAssignment> AddAssignment(SageAssignment assignment);
        SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder);
        SageResponse<SageAssignment> GetAssignments();
        SageResponse<SageAssignment> GetAssignment(long id);
        SageResponse<SageCallType> GetCalltypes();
        SageResponse<SageDepartment> GetDepartments();
        SageResponse<SageAssignment> EditAssignment(SageAssignment assignment);
        SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder);
        SageResponse<SageEmployee> GetEmployees();
        SageResponse<SageEquipment> GetEquipment();
        SageResponse<SageLocation> GetLocations();
        SageResponse<SagePart> GetParts();
        SageResponse<SagePermissionCode> GetPermissionCodes();
        SageResponse<SageProblem> GetProblems();
        SageResponse<SageRateSheet> GetRateSheets();
        SageResponse<SageRepair> GetRepairs();
        SageResponse<SageWorkOrder> UnassignWorkOrders(long id);
        SageResponse<SageWorkOrder> GetWorkorder(long id);
        SageResponse<SageWorkOrder> GetWorkorders();

        SageResponse<SageWorkOrderItem> DeleteWorkOrderItems(long workOrderId, IEnumerable<long> ids);
        SageResponse<SageWorkOrderItem> GetWorkorderItemsByWorkOrderId(long id);
    
        SageResponse<SageWorkOrder> EditWorkOrderStatus(long id, string status);

        SageResponse<SageWorkOrderItem> AddWorkOrderItem(SageWorkOrderItem workOrderItem);
        SageResponse<SageWorkOrderItem> EditWorkOrderItem(SageWorkOrderItem workOrderItem);

        SageResponse<SageNote> AddNote(SageNote note);
        SageResponse<SageNote> EditNote(SageNote note);
        SageResponse<SageNote> GetNotes(long id);
        SageResponse<SageNote> DeleteNote(long id);
    }
}
