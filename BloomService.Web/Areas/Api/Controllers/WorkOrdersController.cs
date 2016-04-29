namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class WorkOrdersController : ApiController
    {
        private readonly IWorkOrderSageApiService workOrderSageApiService;

        public WorkOrdersController(IWorkOrderSageApiService workOrderSageApiService)
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

        public IEnumerable<SageWorkOrder> Post(PropertyDictionary properties)
        {
            return workOrderSageApiService.Add(properties);
        }

        public IEnumerable<SageWorkOrder> Put(PropertyDictionary properties)
        {
            return workOrderSageApiService.Edit(properties);
        }

        public IEnumerable<SageWorkOrder> Delete(PropertyDictionary properties)
        {
            return workOrderSageApiService.Delete(properties);
        }
    }
}
