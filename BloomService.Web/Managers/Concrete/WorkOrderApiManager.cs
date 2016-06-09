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
        }

        public SageResponse<SageWorkOrder> Add(SageWorkOrder workOrder)
        {
            bool isCreate;
            IRestRequest request;
            if(workOrder.WorkOrder == null)
            {
                EndPoint = EndPoints.AddWorkOrder;
                request = new RestRequest(EndPoint, Method.POST);
                isCreate = true;
            }
            else
            {
                EndPoint = EndPoints.EditWorkOrder;
                request = new RestRequest(EndPoint, Method.PUT);
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

        public override SageResponse<SageWorkOrder> Delete(string id)
        {
            EndPoint = EndPoints.UnassignWorkOrders;
            return base.Delete(id);
        }

        public override SageResponse<SageWorkOrder> Get()
        {
            EndPoint = EndPoints.GetWorkorder;
            var request = new RestRequest(EndPoint, Method.GET);
            request.AddHeader("Authorization", token.Token);

            var response = restClient.Execute<SageResponse<SageWorkOrder>>(request);
            var results = response.Data;
            return results;
        }
    }
}