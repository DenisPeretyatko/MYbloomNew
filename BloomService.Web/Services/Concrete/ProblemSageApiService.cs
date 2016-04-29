namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class ProblemSageApiService : SageApiService<SageProblem>, IProblemSageApiService
    {
        private readonly IRestClient restClient;

        private readonly ISession session;

        private readonly IUnitOfWork unitOfWork;

        public ProblemSageApiService(IRestClient restClient, IUnitOfWork unitOfWork /*, ISession session*/)
            : base(restClient, unitOfWork /*, session*/)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;

            // this.session = session;
            EndPoint = ConfigurationManager.AppSettings["ProblemEndPoint"];
        }
    }
}