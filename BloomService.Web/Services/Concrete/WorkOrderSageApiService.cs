using BloomService.Domain.Entities;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Managers;
using BloomService.Web.Utils;
using RestSharp;

namespace BloomService.Web.Services.Concrete
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