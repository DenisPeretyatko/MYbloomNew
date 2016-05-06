namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class WorkOrdersController : ApiController
    {
        private readonly IWorkOrderService workOrderSageApiService;

        public WorkOrdersController(IWorkOrderService workOrderSageApiService)
        {
            this.workOrderSageApiService = workOrderSageApiService;
        }

        public IEnumerable<SageWorkOrder> Get()
        {
            return workOrderSageApiService.Get();
        }

        public SageWorkOrder Get(string id)
        {
            return workOrderSageApiService.Get(id);
        }

        public IEnumerable<SageWorkOrder> Post(SagePropertyDictionary properties)
        {
            return workOrderSageApiService.Add(properties);
        }

        public IEnumerable<SageWorkOrder> Put(SagePropertyDictionary properties)
        {
            return workOrderSageApiService.Edit(properties);
        }
    }
}