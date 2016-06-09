namespace Sage.WebApi.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Exceptions;

    using Sage.WebApi.Infratructure.Service;
    using Sage.WebApi.Utils;
    using System;

    [Authorize]
    public class ServiceManagementApiController : ApiController
    {
        private readonly IServiceManagement serviceManager;

        private readonly IServiceOdbc serviceOdbc;

        public ServiceManagementApiController(IServiceManagement serviceManager, IServiceOdbc serviceOdbc)
        {
            this.serviceManager = serviceManager;
            this.serviceOdbc = serviceOdbc;
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
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/add")]
        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                var properties = SagePropertyConverter.ConvertToProperties(workOrder);
                var resultWorkOrder = serviceManager.WorkOrders(properties).SingleOrDefault();
                //var resultWorkOrder = serviceOdbc.CreateWorkOrder(properties);

                var result = new SageResponse<SageWorkOrder> { IsSucceed = true, Entity = resultWorkOrder };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/assignments/get")]
        public SageResponse<SageAssignment> GetAssignments(string id)
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
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/assignments/get/{id}")]
        public SageResponse<SageAssignment> GetAssignment(string id)
        {
            try
            {
                var result = new SageResponse<SageAssignment>
                {
                    IsSucceed = true,
                    Entity = serviceManager.Assignments(id).FirstOrDefault()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageCallType> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageDepartment> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageAssignment> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/workorders/edit")]
        public SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                serviceOdbc.EditWorkOrder(workOrder);
                var result = new SageResponse<SageWorkOrder> { IsSucceed = true };
                return result;
            }
            catch (Exception exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageEmployee> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageEquipment> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageLocation> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SagePart> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageEntity> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageProblem> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageEntity> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageRepair> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpDelete, Route("api/v2/sm/workorders/unassign/{id}")]
        public SageResponse<SageWorkOrder> UnassignWorkOrders(string id)
        {
            try
            {
                serviceOdbc.UnassignWorkOrder(id);
                var result = new SageResponse<SageWorkOrder> { IsSucceed = true };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Message };
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
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Message };
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
                    Entities = serviceOdbc.WorkOrders().ToList()
                };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse<SageWorkOrder> { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }
    }
}