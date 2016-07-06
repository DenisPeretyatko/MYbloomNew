using System;
using System.Web.Configuration;
using BloomService.Domain.Extensions;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Infrastructure.Services
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Web.Infrastructure.Services.Abstract;
    using BloomService.Web.Managers;
    using BloomService.Web.Services.Concrete;

    using RestSharp;

    public class SageApiProxy : ISageApiProxy
    {
        private readonly IRestClient _restClient;
        private readonly BloomServiceConfiguration _configuration;

        public SageApiProxy(IRestClient restClient, BloomServiceConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
        }

        public SageResponse<SageAssignment> AddAssignment(SageAssignment assignment)
        {
            //return Add(assignment, EndPoints.AddAssignment);
            return new SageResponse<SageAssignment>();
        }

        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            //return Add(workOrder, EndPoints.AddWorkOrder);
            return new SageResponse<SageWorkOrder>();
        }

        public SageResponse<SageAssignment> EditAssignment(SageAssignment assignment)
        {
            //return Edit(assignment, EndPoints.EditAssignment);
            return new SageResponse<SageAssignment>();
        }

        public SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder)
        {
            //return Edit(workOrder, EndPoints.EditWorkOrder);
            return new SageResponse<SageWorkOrder>();
        }

        public SageResponse<SageAssignment> GetAssignment(string id)
        {
            //return Get<SageAssignment>(id, EndPoints.GetAssignments);
            return new SageResponse<SageAssignment>();
        }

        public SageResponse<SageAssignment> GetAssignments()
        {
            //return GetAll<SageAssignment>(EndPoints.GetAssignments);
            return new SageResponse<SageAssignment>();
        }

        public SageResponse<SageCallType> GetCalltypes()
        {
            //return GetAll<SageCallType>(EndPoints.GetCalltypes);
            return new SageResponse<SageCallType>();
        }

        public SageResponse<SageCustomer> GetCustomers()
        {
            //return GetAll<SageCustomer>(EndPoints.GetCustomer);
            return new SageResponse<SageCustomer>();
        }

        public SageResponse<SageDepartment> GetDepartments()
        {
            //return GetAll<SageDepartment>(EndPoints.GetDepartments);
            return new SageResponse<SageDepartment>();
        }

        public SageResponse<SageEmployee> GetEmployees()
        {
            //return GetAll<SageEmployee>(EndPoints.GetEmployees);
            return new SageResponse<SageEmployee>();
        }

        public SageResponse<SageEquipment> GetEquipment()
        {
            //return GetAll<SageEquipment>(EndPoints.GetEquipment);
            return new SageResponse<SageEquipment>();
        }

        public SageResponse<SageLocation> GetLocations()
        {
            //return GetAll<SageLocation>(EndPoints.GetLocations);
            return new SageResponse<SageLocation>();
        }

        public SageResponse<SagePart> GetParts()
        {
            //return GetAll<SagePart>(EndPoints.GetParts);
            return new SageResponse<SagePart>();
        }

        public SageResponse<SagePermissionCode> GetPermissionCodes()
        {
            //return GetAll<SagePermissionCode>(EndPoints.GetPermissionCodes);
            return new SageResponse<SagePermissionCode>();
        }

        public SageResponse<SageProblem> GetProblems()
        {
            //return GetAll<SageProblem>(EndPoints.GetProblems);
            return new SageResponse<SageProblem>();
        }

        public SageResponse<SageRateSheet> GetRateSheets()
        {
            //return GetAll<SageRateSheet>(EndPoints.GetRateSheets);
            return new SageResponse<SageRateSheet>();
        }

        public SageResponse<SageRepair> GetRepairs()
        {
            //return GetAll<SageRepair>(EndPoints.GetRepairs);
            return new SageResponse<SageRepair>();
        }

        public SageResponse<SageWorkOrder> GetWorkorder(string id)
        {
            //return Get<SageWorkOrder>(id, EndPoints.GetWorkorder);
            return new SageResponse<SageWorkOrder>();
        }

        public SageResponse<SageWorkOrder> GetWorkorders()
        {
            //return GetAll<SageWorkOrder>(EndPoints.GetWorkorder);
            return new SageResponse<SageWorkOrder>();
        }

        public SageResponse<SageWorkOrder> UnassignWorkOrders(string id)
        {
            //return Delete<SageWorkOrder>(id, EndPoints.UnassignWorkOrders);
            return new SageResponse<SageWorkOrder>();
        }

        public SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties)
        {
            //return Update<SageWorkOrder>(properties, EndPoints.AddEquipmentToWorkOrder);
            return new SageResponse<SageWorkOrder>();
        }

        private SageResponse<TEntity> Update<TEntity>(Dictionary<string, string> entity, string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint, Method.PUT) {RequestFormat = DataFormat.Json };
            //request.AddBody(entity);
            //BuildAuthenticationHeader(request);
            //var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var results = response.Data;
            //return results;
            return new SageResponse<TEntity>();
        }

        private SageResponse<TEntity> Add<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            //request.AddBody(entity);
            //BuildAuthenticationHeader(request);
            //var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var results = response.Data;
            //return results;
            return new SageResponse<TEntity>();
        }

        private SageResponse<TEntity> Delete<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint + "/{id}", Method.DELETE) { RequestFormat = DataFormat.Json };
            //request.AddUrlSegment("id", id);
            //BuildAuthenticationHeader(request);
            //var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var results = response.Data;
            //return results;
            return new SageResponse<TEntity>();
        }

        private SageResponse<TEntity> Edit<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            //request.AddBody(entity);
            //BuildAuthenticationHeader(request);
            //var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var result = response.Data;
            //return result;
            return new SageResponse<TEntity>();
        }

        private SageResponse<TEntity> Get<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint + "/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            //request.AddUrlSegment("id", id);
            //BuildAuthenticationHeader(request);
            //var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var result = response.Data;
            //return result;
            return new SageResponse<TEntity>();
        }

        private SageResponse<TEntity> GetAll<TEntity>(string endPoint) where TEntity : IEntity
        {
            //var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            //BuildAuthenticationHeader(request);
            // var response = _restClient.Execute<SageResponse<TEntity>>(request);
            //var results = response.Data;
            //return results;
            return new SageResponse<TEntity>();
        }

        private RestRequest BuildAuthenticationHeader(RestRequest request)
        {
            request.AddHeader("Authorization", string.Format("Basic {0}:{1}", _configuration.SageUsername, _configuration.SagePassword));
            return request;
        }
    }
}