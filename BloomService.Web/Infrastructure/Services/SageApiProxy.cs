namespace BloomService.Web.Infrastructure.Services
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Web.Infrastructure.Constants;
    using BloomService.Web.Infrastructure.Services.Interfaces;

    using RestSharp;

    public class SageApiProxy : ISageApiProxy
    {
        private readonly BloomServiceConfiguration configuration;

        private readonly IRestClient restClient;

        public SageApiProxy(IRestClient restClient, BloomServiceConfiguration configuration)
        {
            this.restClient = restClient;
            this.configuration = configuration;
        }

        public SageResponse<SageAssignment> AddAssignment(SageAssignment assignment)
        {
            return Add(assignment, EndPoints.AddAssignment);
        }

        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            return Add(workOrder, EndPoints.AddWorkOrder);
        }

        public SageResponse<SageAssignment> EditAssignment(SageAssignment assignment)
        {
            return Edit(assignment, EndPoints.EditAssignment);
        }

        public SageResponse<SageWorkOrder> EditWorkOrder(SageWorkOrder workOrder)
        {
            return Edit(workOrder, EndPoints.EditWorkOrder);
        }

        public SageResponse<SageWorkOrderItem> AddWorkOrderItem(SageWorkOrderItem workOrderItem)
        {
            return Add(workOrderItem, EndPoints.AddWorkOrderItem);
        }

        public SageResponse<SageWorkOrderItem> EditWorkOrderItem(SageWorkOrderItem workOrderItem)
        {
            return Edit(workOrderItem, EndPoints.EditWorkOrderItem);
        }

        public SageResponse<SageWorkOrderItem> AddOrEditWorkOrderItem(SageWorkOrderItem workOrderItem)
        {
            if (workOrderItem.WorkOrderItem == 0)
            {
                return Add(workOrderItem, EndPoints.AddWorkOrderItem);
            }
            else
            {
                return Edit(workOrderItem, EndPoints.EditWorkOrderItem);
            }
        }

        public SageResponse<SageAssignment> GetAssignment(string id)
        {
            return Get<SageAssignment>(id, EndPoints.GetAssignments);
        }

        public SageResponse<SageAssignment> GetAssignments()
        {
            return GetAll<SageAssignment>(EndPoints.GetAssignments);
        }

        public SageResponse<SageCallType> GetCalltypes()
        {
            return GetAll<SageCallType>(EndPoints.GetCalltypes);
        }

        public SageResponse<SageCustomer> GetCustomers()
        {
            return GetAll<SageCustomer>(EndPoints.GetCustomer);
        }

        public SageResponse<SageDepartment> GetDepartments()
        {
            return GetAll<SageDepartment>(EndPoints.GetDepartments);
        }

        public SageResponse<SageEmployee> GetEmployees()
        {
            return GetAll<SageEmployee>(EndPoints.GetEmployees);
        }

        public SageResponse<SageEquipment> GetEquipment()
        {
            return GetAll<SageEquipment>(EndPoints.GetEquipment);
        }

        public SageResponse<SageLocation> GetLocations()
        {
            return GetAll<SageLocation>(EndPoints.GetLocations);
        }

        public SageResponse<SagePart> GetParts()
        {
            return GetAll<SagePart>(EndPoints.GetParts);
        }

        public SageResponse<SagePermissionCode> GetPermissionCodes()
        {
            return GetAll<SagePermissionCode>(EndPoints.GetPermissionCodes);
        }

        public SageResponse<SageProblem> GetProblems()
        {
            return GetAll<SageProblem>(EndPoints.GetProblems);
        }

        public SageResponse<SageRateSheet> GetRateSheets()
        {
            return GetAll<SageRateSheet>(EndPoints.GetRateSheets);
        }

        public SageResponse<SageRepair> GetRepairs()
        {
            return GetAll<SageRepair>(EndPoints.GetRepairs);
        }

        public SageResponse<SageWorkOrderItem> DeleteWorkOrderItems(int workOrderId, IEnumerable<int> ids)
        {
            var request = new RestRequest(EndPoints.DeleteWorkOrderItems, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("workOrderId", workOrderId.ToString());
            request.AddBody(ids);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<SageWorkOrderItem>>(request);
            var results = response.Data;
            return results;
        }


        public SageResponse<SageWorkOrder> GetWorkorder(string id)
        {
            return Get<SageWorkOrder>(id, EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrder> GetWorkorders()
        {
            return GetAll<SageWorkOrder>(EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrderItem> GetWorkorderItemsByWorkOrderId(string id)
        {
            var request = new RestRequest(EndPoints.GetWorkOrderItemsByWorkorderId, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<SageWorkOrderItem>>(request);
            var result = response.Data;
            return result;
        }

        public SageResponse<SageWorkOrder> UnassignWorkOrders(string id)
        {
            return Delete<SageWorkOrder>(id, EndPoints.UnassignWorkOrders);
        }

        private SageResponse<TEntity> Add<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private RestRequest BuildAuthenticationHeader(RestRequest request)
        {
            request.AddHeader(
                "Authorization", 
                string.Format("Basic {0}:{1}", configuration.SageUsername, configuration.SagePassword));
            return request;
        }

        private SageResponse<TEntity> Delete<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint + "/{id}", Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private SageResponse<TEntity> Edit<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddObject(entity);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }
       
        private SageResponse<TEntity> Get<TEntity>(string id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint + "/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        private SageResponse<TEntity> GetAll<TEntity>(string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private SageResponse<TEntity> Update<TEntity>(Dictionary<string, string> entity, string endPoint)
            where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddObject(entity);
            BuildAuthenticationHeader(request);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }
    }
}