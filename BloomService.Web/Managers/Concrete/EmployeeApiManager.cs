namespace BloomService.Web.Managers.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class EmployeeApiManager : EntityApiManager<SageEmployee>, IEmployeeApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public EmployeeApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;

            GetEndPoint = EndPoints.GetEmployees;
        }
    }
}