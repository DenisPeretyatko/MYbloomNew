using BloomService.Domain.Entities;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Managers;
using BloomService.Web.Services.Abstract;
using BloomService.Web.Utils;
using RestSharp;

namespace BloomService.Web.Services.Concrete
{

    public class CallTypeSageApiService : SageApiService<SageCallType>, ICallTypeSageApiService
    {
        private IRestClient restClient;
        private ISession session;
        private IUnitOfWork unitOfWork;

        public CallTypeSageApiService(IRestClient restClient, IUnitOfWork unitOfWork/*, ISession session*/) : base(restClient, unitOfWork/*, session*/)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;
            //this.session = session;

            EndPoint = System.Configuration.ConfigurationManager.AppSettings["CallTypeEndPoint"]; ;
        }
    }
}