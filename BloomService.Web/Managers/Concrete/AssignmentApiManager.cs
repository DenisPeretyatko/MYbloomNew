namespace BloomService.Web.Managers.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Managers.Concrete.EntityManagers;
    using BloomService.Web.Utils;

    using RestSharp;

    public class AssignmentApiManager : AddableEditableEntityApiManager<SageAssignment>, IAssignmentApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public AssignmentApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;
        }

        public override IEnumerable<SageAssignment> Add(string endPoint, SagePropertyDictionary properties)
        {
            var request = new RestRequest("api/v1/sm/addassignments", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<SageAssignment>>(request);
            var results = response.Data;
            return results;
        }

        public override IEnumerable<SageAssignment> Edit(string endPoint, SagePropertyDictionary properties)
        {
            var request = new RestRequest("api/v1/sm/editassignments", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<SageAssignment>>(request);
            var results = response.Data;
            return results;
        }
    }
}