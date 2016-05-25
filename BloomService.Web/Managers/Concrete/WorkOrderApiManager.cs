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

        public SageResponse Add(string endPoint, SageWorkOrder workOrder)
        {
            endPoint = workOrder.WorkOrder != null ? "api/v2/sm/workorders/edit" : "api/v2/sm/workorders/add";

            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(workOrder);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse>(request);
            var results = response.Data;
            return results;
        }

        public override IEnumerable<SageWorkOrder> Delete(string endPoint, string id)
        {
            endPoint = "api/v1/sm/UnassignWorkOrder";
            return base.Delete(endPoint, id);
        }

        public SageResponse Edit(string endPoint, SageWorkOrder workOrder)
        {
            endPoint = "api/v2/sm/workorders/edit";
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(workOrder);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse>(request);
            var results = response.Data;
            return results;
        }
    }
}