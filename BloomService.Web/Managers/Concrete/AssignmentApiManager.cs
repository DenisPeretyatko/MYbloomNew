namespace BloomService.Web.Managers.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
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

        public override SageResponse<SageAssignment> Add(SageAssignment assignment)
        {
            EndPoint = EndPoints.AddAssignment;
            return base.Add(assignment);
        }

        public override SageResponse<SageAssignment> Edit(SageAssignment assignment)
        {
            EndPoint = EndPoints.EditAssignment;
            return base.Edit(assignment);
        }
    }
}