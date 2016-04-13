using BloomService.Domain.Entities;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Services;
using BloomService.Web.Utils;
using RestSharp;

namespace BloomService.Web.Managers
{
    public class WorkOrderSageApiService : SageApiService<SageWorkOrder>, IWorkOrderSageApiService
    {
        private IRestClient restClient;
        private ISession session;
        private IUnitOfWork unitOfWork;

        public WorkOrderSageApiService(IRestClient restClient, IUnitOfWork unitOfWork/*, ISession session*/) : base(restClient, unitOfWork/*, session*/)
        {
            this.restClient = restClient;
            //this.session = session;
           
            EndPoint = "api/v1/sm/workorders";
        }
    }
}