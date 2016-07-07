﻿namespace Sage.WebApi.Controllers
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
                properties.Add("Assignment", assignment.Assignment.ToString() ?? string.Empty);
                properties.Add("ScheduleDate", assignment.ScheduleDate.ToString() ?? string.Empty);
                properties.Add("Employee", assignment.Employee.ToString() ?? string.Empty);
                properties.Add("WorkOrder", assignment.WorkOrder.ToString() ?? string.Empty);
                properties.Add("EstimatedRepairHours", assignment.EstimatedRepairHours.ToString() ?? string.Empty);
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
        public SageResponse<SageEquipment> GetEquipmentsByWorkOrderId(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    return new SageResponse<SageEquipment>
                    {
                        IsSucceed = false,
                        ErrorMassage = "WorkOrder Id is null or empty."
                    };
                }

                var result = new SageResponse<SageEquipment>
                {
                    IsSucceed = true,
                    Entities = this.serviceManager.GetEquipmentsByWorkOrderId(id).ToList()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageEquipment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
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
                properties.Add("PermissionCode", workOrder.PermissionCode ?? string.Empty);
                properties.Add("PayMethod", workOrder.PayMethod ?? string.Empty);

                var resultProperties = new Dictionary<string, string>();
                foreach (var property in properties)
                {
                    if (!string.IsNullOrEmpty(property.Value))
                    {
                        resultProperties.Add(property.Key, property.Value);
                    }
                }

                var resultWorkOrder = this.serviceManager.WorkOrders(resultProperties).SingleOrDefault();

                var result = new SageResponse<SageWorkOrder> { IsSucceed = true, Entity = resultWorkOrder };
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
                properties.Add("Employee",  assignment.Employee.ToString() ?? string.Empty);
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
                        resultProperties.Add(property.Key, property.Value);
                    }
                }

                this.serviceManager.EditAssignments(resultProperties);
                var result = new SageResponse<SageAssignment> { IsSucceed = true };
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
                var result = new SageResponse<SageWorkOrder>
                {
                    IsSucceed = true,
                    Entity = this.serviceOdbc.EditWorkOrder(workOrder)
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

        [HttpPut, Route("api/v2/sm/workorders/equipment/add")]
        public SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties)
        {
            try
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = this.serviceManager.AddWorkOrderItem(properties) };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }
    }
}