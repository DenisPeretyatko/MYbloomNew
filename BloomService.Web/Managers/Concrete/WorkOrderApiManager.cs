namespace BloomService.Web.Managers.Concrete
{
    using BloomService.Domain.Entities.Concrete;
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
    }
}