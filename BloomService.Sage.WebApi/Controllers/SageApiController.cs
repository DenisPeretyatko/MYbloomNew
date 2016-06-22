using System.Web.Configuration;
using Sage.WebApi.Infratructure.Atributes;

namespace Sage.WebApi.Areas.Api.Controllers
{
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    using Infratructure.Service;
    using Utils;
    using System.Collections.Generic;
    using System.Web.Http;
    using BloomService.Domain.Models.Requests;
    using BloomService.Domain.Models.Responses;
    using WebGrease.Css.Extensions;
    using Infratructure.MessageResponse;

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

        [HttpPost, Route("api/v2/Authorization/Authorization")]
        public AuthorizationResponse Authorization(AuthorizationRequest model)
        {
            var response = serviceAuthorization.Authorization(model);
            return response;
        }


        [HttpGet, Route("api/v2/ar/customers")]
        public SageResponse<SageCustomer> Customers()
        {
            try
            {
                var result = new SageResponse<SageCustomer> { IsSucceed = true, Entities = serviceOdbc.Customers() };
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
                var properties = SagePropertyConverter.ConvertToProperties(assignment);
                var resultAssignment = serviceManager.AddAssignments(properties).SingleOrDefault();
                var result = new SageResponse<SageAssignment> { IsSucceed = true, Entity = resultAssignment };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/add")]
        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                var properties = SagePropertyConverter.ConvertToProperties(workOrder);
                var requestProperties = new Dictionary<string, string>();
                var resultProperties = new Dictionary<string, string>();

                requestProperties.Add("ARCustomer", properties.ContainsKey("ARCustomer") ? properties["ARCustomer"] : string.Empty);
                requestProperties.Add("Location", properties.ContainsKey("Location") ? properties["Location"] : string.Empty);
                requestProperties.Add("CallType", properties.ContainsKey("CallType") ? properties["CallType"] : string.Empty);
                requestProperties.Add("CallDate", properties.ContainsKey("CallDate") ? properties["CallDate"] : string.Empty);
                requestProperties.Add("Problem", properties.ContainsKey("Problem") ? properties["Problem"] : string.Empty);
                requestProperties.Add("RateSheet", properties.ContainsKey("RateSheet") ? properties["RateSheet"] : string.Empty);
                requestProperties.Add("Employee", properties.ContainsKey("Employee") ? properties["Employee"] : string.Empty);
                requestProperties.Add("Equipment", properties.ContainsKey("Equipment") ? properties["Equipment"] : string.Empty);
                requestProperties.Add("EstimatedRepairHours", properties.ContainsKey("EstimatedRepairHours") ? properties["EstimatedRepairHours"] : string.Empty);
                requestProperties.Add("NottoExceed", properties.ContainsKey("NottoExceed") ? properties["NottoExceed"] : string.Empty);
                requestProperties.Add("Comments", properties.ContainsKey("Comments") ? properties["Comments"] : string.Empty);
                requestProperties.Add("CustomerPO", properties.ContainsKey("CustomerPO") ? properties["CustomerPO"] : string.Empty);
                requestProperties.Add("PermissionCode", properties.ContainsKey("PermissionCode") ? properties["PermissionCode"] : string.Empty);
                requestProperties.Add("PayMethod", properties.ContainsKey("PayMethod") ? properties["PayMethod"] : string.Empty);

                requestProperties.ForEach(x =>
                {
                    if (x.Value != string.Empty)
                    {
                        resultProperties.Add(x.Key, x.Value);
                    }
                });

                var resultWorkOrder = serviceManager.WorkOrders(resultProperties).SingleOrDefault();

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
                    Entities = serviceManager.Assignments().ToList()
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
                var assignments = serviceManager.Assignments(id);
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
                var result = new SageResponse<SageCallType> { IsSucceed = true, Entities = serviceManager.Calltypes().ToList() };
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
                var result = new SageResponse<SageDepartment> { IsSucceed = true, Entities = serviceManager.Departments().ToList() };
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
                var properties = SagePropertyConverter.ConvertToProperties(assignment);
                serviceManager.EditAssignments(properties);
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
                    Entity = serviceOdbc.EditWorkOrder(workOrder)
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
                var result = new SageResponse<SageEmployee> { IsSucceed = true, Entities = serviceManager.Employees().ToList() };
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
                var result = new SageResponse<SageEquipment> { IsSucceed = true, Entities = serviceManager.Equipments().ToList() };
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
                var result = new SageResponse<SageLocation> { IsSucceed = true, Entities = serviceManager.Locations().ToList() };
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
                var result = new SageResponse<SagePart> { IsSucceed = true, Entities = serviceManager.Parts().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SagePart> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/permissioncodes/get")]
        public SageResponse<SageEntity> GetPermissionCodes()
        {
            try
            {
                var result = new SageResponse<SageEntity> { IsSucceed = true, Strings = serviceManager.PermissionCode().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageEntity> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/problems/get")]
        public SageResponse<SageProblem> GetProblems()
        {
            try
            {
                var result = new SageResponse<SageProblem> { IsSucceed = true, Entities = serviceManager.Problems().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageProblem> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/ratesheets/get")]
        public SageResponse<SageEntity> GetRateSheets()
        {
            try
            {
                var result = new SageResponse<SageEntity> { IsSucceed = true, Strings = serviceManager.RateSheet().ToList() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageEntity> { IsSucceed = false, ErrorMassage = exception.Error.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/repairs/get")]
        public SageResponse<SageRepair> GetRepairs()
        {
            try
            {
                var result = new SageResponse<SageRepair> { IsSucceed = true, Entities = serviceManager.Repairs().ToList() };
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
                if(id == string.Empty)
                {
                    throw new ResponseException(new ResponseError() { Message = "WorkOrder id is empty"});
                }
                serviceOdbc.UnassignWorkOrder(id);
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
                    Entity = serviceManager.WorkOrders(id).SingleOrDefault()
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
                    Entities = serviceManager.WorkOrders().ToList()
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
                var result = new SageResponse<SageWorkOrder> { IsSucceed = serviceManager.AddWorkOrderItem(properties) };
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