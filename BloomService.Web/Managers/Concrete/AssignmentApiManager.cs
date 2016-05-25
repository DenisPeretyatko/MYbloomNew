namespace BloomService.Web.Managers.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class AssignmentApiManager : EntityApiManager<SageAssignment>, IAssignmentApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public AssignmentApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;
        }

        public override IEnumerable<SageAssignment> Add(string endPoint, Dictionary<string, string> properties)
        {
            endPoint = "api/v1/sm/addassignments";
            return base.Add(endPoint, properties);
        }
       
        public override IEnumerable<SageAssignment> Edit(string endPoint, Dictionary<string, string> properties)
        {
            endPoint = "api/v1/sm/editassignments";
            return base.Edit(endPoint, properties);
        }
    }
}