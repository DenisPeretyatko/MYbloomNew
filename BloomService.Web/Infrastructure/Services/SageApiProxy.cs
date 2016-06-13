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

        private readonly string token;

        public SageApiProxy(IRestClient restClient)
        {
            _restClient = restClient;
            this.token = AuthorizationService.GetAuthToken();
        }

        public SageResponse<SageAssignment> AddAssignment(SageAssignment assignment)
        {
            return this.Add(assignment, EndPoints.AddAssignment);
        }

        public SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties)
        {
            var request = new RestRequest(EndPoints.AddEquipmentToWorkOrder, Method.PUT)
                              {
                                  RequestFormat = DataFormat.Json
                              };
            request.AddBody(properties);
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<SageWorkOrder>>(request);
            var results = response.Data;
            return results;
        }

        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            return this.Add(workOrder, EndPoints.AddWorkOrder);
        }

        public SageResponse<SageAssignment> EditAssignment(SageAssignment assignment)
        {
            return this.Edit(assignment, EndPoints.EditAssignment);
        }

        public SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder)
        {
            return this.Edit(workOrder, EndPoints.EditWorkOrder);
        }

        public SageResponse<SageAssignment> GetAssignment(string id)
        {
            return this.Get<SageAssignment>(id, EndPoints.GetAssignments);
        }

        public SageResponse<SageAssignment> GetAssignments()
        {
            return this.GetAll<SageAssignment>(EndPoints.GetAssignments);
        }

        public SageResponse<SageCallType> GetCalltypes()
        {
            return this.GetAll<SageCallType>(EndPoints.GetCalltypes);
        }

        public SageResponse<SageCustomer> GetCustomers()
        {
            return this.GetAll<SageCustomer>(EndPoints.GetCustomer);
        }

        public SageResponse<SageDepartment> GetDepartments()
        {
            return this.GetAll<SageDepartment>(EndPoints.GetDepartments);
        }

        public SageResponse<SageEmployee> GetEmployees()
        {
            return this.GetAll<SageEmployee>(EndPoints.GetEmployees);
        }

        public SageResponse<SageEquipment> GetEquipment()
        {
            return this.GetAll<SageEquipment>(EndPoints.GetEquipment);
        }

        public SageResponse<SageLocation> GetLocations()
        {
            return this.GetAll<SageLocation>(EndPoints.GetLocations);
        }

        public SageResponse<SagePart> GetParts()
        {
            return this.GetAll<SagePart>(EndPoints.GetParts);
        }

        public SageResponse<SageEntity> GetPermissionCodes()
        {
            return this.GetAll<SageEntity>(EndPoints.GetPermissionCodes);
        }

        public SageResponse<SageProblem> GetProblems()
        {
            return this.GetAll<SageProblem>(EndPoints.GetProblems);
        }

        public SageResponse<SageEntity> GetRateSheets()
        {
            return this.GetAll<SageEntity>(EndPoints.GetRateSheets);
        }

        public SageResponse<SageRepair> GetRepairs()
        {
            return this.GetAll<SageRepair>(EndPoints.GetRepairs);
        }

        public SageResponse<SageWorkOrder> GetWorkorder(string id)
        {
            return this.Get<SageWorkOrder>(id, EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrder> GetWorkorders()
        {
            return this.GetAll<SageWorkOrder>(EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrder> UnassignWorkOrders(string id)
        {
            return this.Get<SageWorkOrder>(id, EndPoints.UnassignWorkOrders);
        }

        private SageResponse<TEntity> Add<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private SageResponse<TEntity> Delete<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private SageResponse<TEntity> Edit<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        private SageResponse<TEntity> Get<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        private SageResponse<TEntity> GetAll<TEntity>(string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", this.token);
            var response = this._restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }
    }
}