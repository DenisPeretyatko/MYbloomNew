using System.Collections.Generic;
using BloomService.Domain.Entities.Abstract;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Entities.Concrete.Auxiliary;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Web.Infrastructure.Services.Interfaces;

namespace BloomService.Web.Infrastructure.Services
{
    using RestSharp;
    public class SageApiProxy : ISageApiProxy
    {
        private readonly BloomServiceConfiguration _configuration;
        private readonly IRestClient _restClient;

        public SageApiProxy(IRestClient restClient, BloomServiceConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
        }

        public SageResponse<SageAssignment> AddAssignment(SageAssignment assignment)
        {
            return Add(assignment, EndPoints.AddAssignment);
        }

        public SageResponse<SageNote> AddNote(SageNote note)
        {
            return Add(note, EndPoints.CreateNote);
        }

        public SageResponse<SageNote> EditNote(SageNote note)
        {
            return Edit(note, EndPoints.EditNote);
        }

        public SageResponse<SageNote> GetNotes(long id)
        {
            return Get<SageNote>(id, EndPoints.GetNotes);
        }

        public SageResponse<SageWorkOrderLocationAccordance> GetAccordance()
        {
            return GetAll<SageWorkOrderLocationAccordance>(EndPoints.GetAccordance);
        }

        public SageResponse<SageNote> DeleteNote(long id)
        {
            return Delete<SageNote>(id, EndPoints.RemoveNote);
        }

        public SageResponse<SageWorkOrder> MarkAsReviewed(long id)
        {
            return Delete<SageWorkOrder>(id, EndPoints.MarkAsReviewed);
        }

        public SageResponse<SageNote> DeleteNotes(IEnumerable<long> ids)
        {
            var request = new RestRequest(EndPoints.DeleteNotes, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddBody(ids);
            BuildAuthenticationHeader(request);
            var response = this._restClient.Execute<SageResponse<SageNote>>(request);
            var results = response.Data;
            return results;
        }

        public SageResponse<SageWorkOrder> AddWorkOrder(SageWorkOrder workOrder)
        {
            return Add(workOrder, EndPoints.AddWorkOrder);
        }

        public SageResponse<SageAssignment> EditAssignment(SageAssignment assignment)
        {
            return Edit(assignment, EndPoints.EditAssignment);
        }

        public SageResponse<SageWorkOrder> EditWorkOrderStatus(long id, string status)
        {
            var request = new RestRequest(EndPoints.EditWorkOrderStatus + "/{id}/{status}", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id.ToString());
            request.AddUrlSegment("status", status);
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<SageWorkOrder>>(request);
            var results = response.Data;
            return results;
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
            return workOrderItem.WorkOrderItem == 0 ? 
                Add(workOrderItem, EndPoints.AddWorkOrderItem) :
                Edit(workOrderItem, EndPoints.EditWorkOrderItem);
        }

        public SageResponse<SageAssignment> GetAssignment(long id)
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

        public SageResponse<SageNote> GetNotes()
        {
            return GetAll<SageNote>(EndPoints.GetNotes);
        }

        public SageResponse<SageWorkOrderItem> GetItems()
        {
            var request = new RestRequest(EndPoints.GetItems, Method.GET) { RequestFormat = DataFormat.Json, AlwaysMultipartFormData = true};
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<SageWorkOrderItem>>(request);
            var results = response.Data;
            return results;
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

        public SageResponse<SageWorkOrderItem> DeleteWorkOrderItems(long workOrderId, IEnumerable<long> ids)
        {
            var request = new RestRequest(EndPoints.DeleteWorkOrderItems, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("workOrderId", workOrderId.ToString());
            request.AddBody(ids);
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<SageWorkOrderItem>>(request);
            var results = response.Data;
            return results;
        }

        public SageResponse<SageWorkOrder> GetWorkorder(long id)
        {
            return Get<SageWorkOrder>(id, EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrder> GetWorkorders()
        {
            return GetAll<SageWorkOrder>(EndPoints.GetWorkorder);
        }

        public SageResponse<SageWorkOrderItem> GetWorkorderItemsByWorkOrderId(long id)
        {
            var request = new RestRequest(EndPoints.GetWorkOrderItemsByWorkorderId, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id.ToString());
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<SageWorkOrderItem>>(request);
            var result = response.Data;
            return result;
        }

        public SageResponse<SageWorkOrder> UnassignWorkOrders(long id)
        {
            return Delete<SageWorkOrder>(id, EndPoints.UnassignWorkOrders);
        }

        private SageResponse<TEntity> Add<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private RestRequest BuildAuthenticationHeader(RestRequest request)
        {
            request.AddHeader("Authorization",
                $"Basic {_configuration.SageUsername}:{_configuration.SagePassword}");
            return request;
        }

        private SageResponse<TEntity> Delete<TEntity>(long id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint + "/{id}", Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id.ToString());
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        private SageResponse<TEntity> Edit<TEntity>(TEntity entity, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddObject(entity);
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        private SageResponse<TEntity> Get<TEntity>(long id, string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint + "/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id.ToString());
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        private SageResponse<TEntity> GetAll<TEntity>(string endPoint) where TEntity : IEntity
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            BuildAuthenticationHeader(request);
            var response = _restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        //private SageResponse<TEntity> Update<TEntity>(Dictionary<string, string> entity, string endPoint)
        //    where TEntity : IEntity
        //{
        //    var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
        //    request.AddObject(entity);
        //    BuildAuthenticationHeader(request);
        //    var response = _restClient.Execute<SageResponse<TEntity>>(request);
        //    var results = response.Data;
        //    return results;
        //}
    }
}