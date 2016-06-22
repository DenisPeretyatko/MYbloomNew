﻿using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Entities.Concrete.Auxiliary;
using System.Collections.Generic;

namespace BloomService.Web.Infrastructure.Services.Abstract
{
    public interface ISageApiProxy
    {
        SageResponse<SageCustomer> GetCustomers();
        SageResponse<SageAssignment> AddAssignment(SageAssignment assignment);
        SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder);
        SageResponse<SageAssignment> GetAssignments();
        SageResponse<SageAssignment> GetAssignment(string id);
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
        SageResponse<SageWorkOrder> UnassignWorkOrders(string id);
        SageResponse<SageWorkOrder> GetWorkorder(string id);
        SageResponse<SageWorkOrder> GetWorkorders();
        SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties);
    }
}
