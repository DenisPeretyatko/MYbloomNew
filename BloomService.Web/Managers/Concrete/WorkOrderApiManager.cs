namespace BloomService.Web.Managers.Concrete
{
    using System;
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class WorkOrderApiManager : EntityApiManager<SageWorkOrder>, IWorkOrderApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public WorkOrderApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;

            CreateEndPoint = EndPoints.AddWorkOrder;
            EditEndPoint = EndPoints.EditWorkOrder;
            DeleteEndPoint = EndPoints.UnassignWorkOrders;
            GetEndPoint = EndPoints.GetWorkorder;
        }

        public override SageResponse<SageWorkOrder> Add(SageWorkOrder workOrder)
        {
            bool isCreate;
            IRestRequest request;
            if(workOrder.WorkOrder == null)
            {
                request = new RestRequest(CreateEndPoint, Method.POST);
                isCreate = true;
            }
            else
            {
                request = new RestRequest(EditEndPoint, Method.PUT);
            }

            request.RequestFormat = DataFormat.Json;
                       
            request.AddObject(workOrder);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<SageWorkOrder>>(request);
            var results = response.Data;
            if(results != null && results.IsSucceed)
            {

            }
            return results;
        }

        public SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties)
        {
            var request = new RestRequest(CreateEndPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<SageWorkOrder>>(request);
            var results = response.Data;
            return results;
        }
    }
}