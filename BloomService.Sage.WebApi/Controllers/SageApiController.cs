namespace Sage.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Models.Requests;
    using BloomService.Domain.Models.Responses;

    using Sage.WebApi.Infratructure.Atributes;
    using Sage.WebApi.Infratructure.MessageResponse;
    using Sage.WebApi.Infratructure.Service;
    using Sage.WebApi.Models;
    using BloomService.Domain.Extensions;
    [BasicAuthentication]
    public class SageApiController : ApiController
    {
        private readonly IServiceManagement serviceManager;

        private readonly IServiceOdbc serviceOdbc;

        private readonly IServiceAuthorization serviceAuthorization;

        public SageApiController(IServiceManagement serviceManager, IServiceOdbc serviceOdbc, IServiceAuthorization serviceAuthorization)
        {
            this.serviceManager = serviceManager;
            this.serviceOdbc = serviceOdbc;
            this.serviceAuthorization = serviceAuthorization;
        }

        [HttpPost, Route("api/v2/Xml/Test")]
        public string Test(TestXmlModel model)
        {
            var response = this.serviceManager.SendMessageXml(model.Model);
            return response;
        }

        [HttpPost, Route("api/v2/Authorization/Authorization")]
        public AuthorizationResponse Authorization(AuthorizationRequest model)
        {
            var response = this.serviceAuthorization.Authorization(model);
            return response;
        }

        [HttpGet, Route("api/v2/ar/customers")]
        public SageResponse<SageCustomer> Customers()
        {
            try
            {
                var result = new SageResponse<SageCustomer> { IsSucceed = true, Entities = this.serviceOdbc.Customers() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageCustomer> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/assignments/add")]
        public SageResponse<SageAssignment> AddAssignment(SageAssignment assignment)
        {
            try
            {
                var properties = new Dictionary<string, string>();
                properties.Add("Assignment", assignment.Assignment.ToString());
                properties.Add("ScheduleDate", ((DateTime)assignment.ScheduleDate).ToString() ?? string.Empty);
                properties.Add("Employee", assignment.Employee ?? string.Empty);
                properties.Add("WorkOrder", assignment.WorkOrder.ToString());
                properties.Add("EstimatedRepairHours", assignment.EstimatedRepairHours ?? string.Empty);
                properties.Add("StartTime", ((DateTime)assignment.StartTime).TimeOfDay.ToString() ?? string.Empty);
                properties.Add("Enddate", assignment.Enddate.ToString() ?? string.Empty);
                properties.Add("Endtime", ((DateTime)assignment.Endtime).TimeOfDay.ToString() ?? string.Empty);

                var resultProperties = new Dictionary<string, string>();
                foreach (var property in properties)
                {
                    if (property.Value != string.Empty && property.Value != null)
                    {
                        resultProperties.Add(property.Key, property.Value);
                    }
                }

                var resultAssignment = this.serviceManager.AddAssignments(resultProperties).SingleOrDefault();
                var result = new SageResponse<SageAssignment> { IsSucceed = true, Entity = resultAssignment };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorder/{id}/equipments")]
        public SageResponse<SageWorkOrderItem> GetEquipmentsByWorkOrderId(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    return new SageResponse<SageWorkOrderItem>
                    {
                        IsSucceed = false,
                        ErrorMassage = "WorkOrder Id is null or empty."
                    };
                }

                var result = new SageResponse<SageWorkOrderItem>
                {
                    IsSucceed = true,
                    Entities = serviceManager.GetEquipmentsByWorkOrderId(id)?.ToList()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/add")]
        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                var properties = new Dictionary<string, string>();

                properties.Add("ARCustomer", workOrder.ARCustomer ?? string.Empty);
                properties.Add("Location", workOrder.Location ?? string.Empty);
                properties.Add("CallType", workOrder.CallType ?? string.Empty);
                properties.Add("CallDate", workOrder.CallDate.ToString() ?? string.Empty);
                properties.Add("Problem", workOrder.Problem ?? string.Empty);
                properties.Add("RateSheet", workOrder.RateSheet ?? string.Empty);
                properties.Add("Employee", workOrder.Employee ?? string.Empty);
                properties.Add("Equipment", workOrder.Equipment.ToString() ?? string.Empty);
                properties.Add("EstimatedRepairHours", workOrder.EstimatedRepairHours.ToString() ?? string.Empty);
                properties.Add("NottoExceed", workOrder.NottoExceed ?? string.Empty);
                properties.Add("Comments", workOrder.Comments ?? string.Empty);
                properties.Add("CustomerPO", workOrder.CustomerPO ?? string.Empty);
                properties.Add("Contact", workOrder.Contact ?? string.Empty);
                properties.Add("PermissionCode", workOrder.PermissionCode ?? string.Empty);
                properties.Add("PayMethod", workOrder.PayMethod ?? string.Empty);
                properties.Add("JCJob", workOrder.JCJob ?? string.Empty);

                var resultProperties = new Dictionary<string, string>();
                foreach (var property in properties)
                {
                    if (!string.IsNullOrEmpty(property.Value))
                    {
                        resultProperties.Add(property.Key, property.Value.Sanitize());
                    }
                }

                var resultWorkOrder = this.serviceManager.WorkOrders(resultProperties).SingleOrDefault();
                var resultAssignment = new SageAssignment();
                if (resultWorkOrder != null)
                {
                   resultAssignment = this.serviceManager.GetAssignmentByWorkOrderId(resultWorkOrder.WorkOrder.ToString()).SingleOrDefault();
                }

                var result = new SageResponse<SageWorkOrder> { IsSucceed = true, Entity = resultWorkOrder, RelatedAssignment = resultAssignment };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/assignments/get/")]
        public SageResponse<SageAssignment> GetAssignments()
        {
            try
            {
                var result = new SageResponse<SageAssignment>
                {
                    IsSucceed = true,
                    Entities = this.serviceManager.Assignments().ToList()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/assignments/get/{id}")]
        public SageResponse<SageAssignment> GetAssignment(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    return new SageResponse<SageAssignment>
                    {
                        IsSucceed = false,
                        ErrorMassage = "Assignment Id is null or empty."
                    };
                }

                var assignments = this.serviceManager.Assignments(id);
                var result = new SageResponse<SageAssignment>
                {
                    IsSucceed = true,
                    Entity = assignments == null ? null : assignments.FirstOrDefault()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/calltypes/get")]
        public SageResponse<SageCallType> GetCalltypes()
        {
            try
            {
                var result = new SageResponse<SageCallType> { IsSucceed = true, Entities = this.serviceManager.Calltypes().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageCallType> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/departments/get")]
        public SageResponse<SageDepartment> GetDepartments()
        {
            try
            {
                var result = new SageResponse<SageDepartment> { IsSucceed = true, Entities = this.serviceManager.Departments().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageDepartment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/assignments/edit")]
        public SageResponse<SageAssignment> EditAssignment(SageAssignment assignment)
        {
            try
            {
                var properties = new Dictionary<string, string>();
                properties.Add("Assignment", assignment.Assignment.ToString() ?? string.Empty);
                properties.Add("ScheduleDate", assignment.ScheduleDate.ToString() ?? string.Empty);
                properties.Add("Employee",  assignment.Employee ?? string.Empty);
                properties.Add("WorkOrder",  assignment.WorkOrder.ToString() ?? string.Empty);
                properties.Add("EstimatedRepairHours",  assignment.EstimatedRepairHours.ToString() ?? string.Empty);
                properties.Add("StartTime",((DateTime)assignment.StartTime).TimeOfDay.ToString() ?? string.Empty);
                properties.Add("Enddate",  assignment.Enddate.ToString() ?? string.Empty);
                properties.Add("Endtime",  ((DateTime)assignment.Endtime).TimeOfDay.ToString() ?? string.Empty);

                var resultProperties = new Dictionary<string, string>();
                foreach (var property in properties)
                {
                    if (!string.IsNullOrEmpty(property.Value))
                    {
                        resultProperties.Add(property.Key, property.Value.Sanitize());
                    }
                }

                var resultAssignment = serviceManager.EditAssignments(resultProperties).SingleOrDefault();
                var result = new SageResponse<SageAssignment> { IsSucceed = true, Entity = resultAssignment };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/workorders/edit")]
        public SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                serviceOdbc.EditWorkOrder(workOrder);
                var result = new SageResponse<SageWorkOrder>
                {
                    IsSucceed = true,
                    Entity = serviceManager.WorkOrders(workOrder.WorkOrder.ToString()).SingleOrDefault()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/workorders/notes/edit")]
        public SageResponse<SageNote> EditWorkOrderNote(SageNote note)
        {
            try
            {
                serviceOdbc.EditNote(note);
                var result = new SageResponse<SageNote>
                {
                    IsSucceed = true
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageNote> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/notes/add")]
        public SageResponse<SageNote> CreateWorkOrderNote(SageNote note)
        {
            try
            {
                serviceOdbc.AddNote(note);
                var result = new SageResponse<SageNote>
                {
                    IsSucceed = true
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageNote> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpDelete, Route("api/v2/sm/workorders/notes/remove/{id}")]
        public SageResponse<SageNote> DeleteWorkOrderNote(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    return new SageResponse<SageNote>
                    {
                        IsSucceed = false,
                        ErrorMassage = "Note Id is null or empty."
                    };
                }

                serviceOdbc.DeleteNote(id);
                var result = new SageResponse<SageNote>
                {
                    IsSucceed = true
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageNote> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorders/notes/{id}")]
        public SageResponse<SageNote> GetWorkOrderNotes(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    return new SageResponse<SageNote>
                    {
                        IsSucceed = false,
                        ErrorMassage = "WorkOrder Id is null or empty."
                    };
                }

                var result = new SageResponse<SageNote>
                {
                    IsSucceed = true,
                    Entities = serviceOdbc.GetNotes(id)
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageNote> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/status/edit/{id}/{status}")]
        public SageResponse<SageWorkOrder> EditWorkOrderStatus(string id, string status)
        {
            try
            {
                serviceOdbc.EditWorkOrderStatus(id, status);
                var result = new SageResponse<SageWorkOrder>
                {
                    IsSucceed = true
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/employees/get")]
        public SageResponse<SageEmployee> GetEmployees()
        {
            try
            {
                var result = new SageResponse<SageEmployee> { IsSucceed = true, Entities = this.serviceManager.Employees().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageEmployee> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/equipment/get")]
        public SageResponse<SageEquipment> GetEquipment()
        {
            try
            {
                var result = new SageResponse<SageEquipment> { IsSucceed = true, Entities = this.serviceManager.Equipments().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageEquipment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/location/get")]
        public SageResponse<SageLocation> GetLocations()
        {
            try
            {
                var result = new SageResponse<SageLocation> { IsSucceed = true, Entities = this.serviceManager.Locations().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageLocation> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/parts/get")]
        public SageResponse<SagePart> GetParts()
        {
            try
            {
                var result = new SageResponse<SagePart> { IsSucceed = true, Entities = this.serviceManager.Parts().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SagePart> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/permissioncodes/get")]
        public SageResponse<SagePermissionCode> GetPermissionCodes()
        {
            try
            {
                var result = new SageResponse<SagePermissionCode> { IsSucceed = true, Entities = this.serviceOdbc.PermissionCodes() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SagePermissionCode> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/problems/get")]
        public SageResponse<SageProblem> GetProblems()
        {
            try
            {
                var result = new SageResponse<SageProblem> { IsSucceed = true, Entities = this.serviceManager.Problems().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageProblem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/ratesheets/get")]
        public SageResponse<SageRateSheet> GetRateSheets()
        {
            try
            {
                var result = new SageResponse<SageRateSheet> { IsSucceed = true, Entities = this.serviceOdbc.RateSheets() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageRateSheet> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/repairs/get")]
        public SageResponse<SageRepair> GetRepairs()
        {
            try
            {
                var result = new SageResponse<SageRepair> { IsSucceed = true, Entities = this.serviceManager.Repairs().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageRepair> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpDelete, Route("api/v2/sm/workorders/unassign/{id}")]
        public SageResponse<SageWorkOrder> UnassignWorkOrders(string id)
        {
            try
            {
                if (id == string.Empty)
                {
                    throw new ResponseException(new ResponseError() { Message = "WorkOrder id is empty" });
                }
                this.serviceOdbc.UnassignWorkOrder(id);
                var result = new SageResponse<SageWorkOrder> { IsSucceed = true };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorders/get/{id}")]
        public SageResponse<SageWorkOrder> GetWorkorders(string id)
        {
            try
            {
                var result = new SageResponse<SageWorkOrder>
                {
                    IsSucceed = true,
                    Entity = this.serviceManager.WorkOrders(id).SingleOrDefault()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorders/get")]
        public SageResponse<SageWorkOrder> GetWorkorders()
        {
            try
            {
                var result = new SageResponse<SageWorkOrder>
                {
                    IsSucceed = true,
                    Entities = this.serviceManager.WorkOrders().ToList()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/equipment/add")]
        public SageResponse<SageWorkOrderItem> AddEquipment(SageWorkOrderItem workOrderItem)
        {
            try
            {
                var woItem = serviceManager.AddWorkOrderItem(workOrderItem).Single();
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = true, Entity = woItem };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/workorders/equipment/edit")]
        public SageResponse<SageWorkOrderItem> EditEquipment(SageWorkOrderItem workOrderItem)
        {
            try
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = true, Entity = serviceManager.EditWorkOrderItem(workOrderItem).Single() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorders/workorderitems/get")]
        public SageResponse<SageWorkOrderItem> GetWorkOrderItems()
        {
            try
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = true, Entities = this.serviceManager.WorkOrderItems().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpDelete, Route("api/v2/sm/workorders/workorderitems/delete")]
        public SageResponse<SageWorkOrderItem> DeleteWorkOrderItems(int workOrderId, IEnumerable<int> ids)
        {
            try
            {
                serviceOdbc.DeleteWorkOrderItems(workOrderId, ids);
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = true };
                return result;
            }
            catch (Exception exception)
            {
                var result = new SageResponse<SageWorkOrderItem> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }
    }
}