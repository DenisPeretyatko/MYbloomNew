namespace BloomService.Web.Managers.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class CallTypeApiManager : EntityApiManager<SageCallType>, ICallTypeApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public CallTypeApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;

            GetEndPoint = EndPoints.GetCalltypes;
        }
    }
}