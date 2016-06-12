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

            GetEndPoint = EndPoints.GetAssignments;
            CreateEndPoint = EndPoints.AddAssignment;
            EditEndPoint = EndPoints.EditAssignment;
        }
    }
}