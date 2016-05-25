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
        public SageResponse AddAssignment(SageAssignment assignment)
        {
            try
            {
                var properties = SagePropertyConverter.ConvertToProperties(assignment);
                var resultAssignment = serviceManager.AddAssignments(properties).SingleOrDefault();
                var result = new SageResponse { IsSucceed = true, Entity = resultAssignment };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpPost, Route("api/v2/sm/workorders/add")]
        public SageResponse AddWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                var properties = SagePropertyConverter.ConvertToProperties(workOrder);
                var resultWorkOrder = serviceManager.WorkOrders(properties).SingleOrDefault();

                var result = new SageResponse { IsSucceed = true, Entity = resultWorkOrder };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/assignments/get/{id}")]
        public SageResponse GetAssignments(string id)
        {
            try
            {
                var result = new SageResponse
                                 {
                                     IsSucceed = true, 
                                     Entities =
                                         id == null
                                             ? serviceManager.Assignments()
                                             : serviceManager.Assignments(id)
                                 };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/calltypes/get")]
        public SageResponse GetCalltypes()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Calltypes() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/departments/get")]
        public SageResponse GetDepartments()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Departments() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/assignments/edit")]
        public SageResponse EditAssignment(SageAssignment assignment)
        {
            try
            {
                var properties = SagePropertyConverter.ConvertToProperties(assignment);
                serviceManager.EditAssignments(properties);
                var result = new SageResponse { IsSucceed = true };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpPut, Route("api/v2/sm/workorders/edit")]
        public SageResponse EditWorkOrder(SageWorkOrder workOrder)
        {
            try
            {
                serviceOdbc.EditWorkOrder(workOrder);
                var result = new SageResponse { IsSucceed = true };
                return result;
            }
            catch (Exception exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/employees/get")]
        public SageResponse GetEmployees()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Employees() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/equipment/get")]
        public SageResponse GetEquipment()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Equipments() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/location/get")]
        public SageResponse GetLocations()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Locations() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/parts/get")]
        public SageResponse GetParts()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Parts() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/permissioncodes/get")]
        public SageResponse GetPermissionCodes()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Strings = serviceManager.PermissionCode() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/problems/get")]
        public SageResponse GetProblems()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Problems() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/ratesheets/get")]
        public SageResponse GetRateSheets()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Strings = serviceManager.RateSheet() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/repairs/get")]
        public SageResponse GetRepairs()
        {
            try
            {
                var result = new SageResponse { IsSucceed = true, Entities = serviceManager.Repairs() };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpDelete, Route("api/v2/sm/workorders/get/{id}")]
        public SageResponse UnassignWorkOrders(string id)
        {
            try
            {
                serviceOdbc.UnassignWorkOrder(id);
                var result = new SageResponse { IsSucceed = true };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }

        [HttpGet, Route("api/v2/sm/workorders/get/{id}")]
        public SageResponse GetWorkorders(string id)
        {
            try
            {
                var result = new SageResponse
                                 {
                                     IsSucceed = true, 
                                     Entities =
                                         id == null
                                             ? serviceManager.WorkOrders()
                                             : serviceManager.WorkOrders(id)
                                 };
                return result;
            }
            catch (ResponseException exception)
            {
                var result = new SageResponse { IsSucceed = false, ErrorMassage = exception.Message };
                return result;
            }
        }
    }
}