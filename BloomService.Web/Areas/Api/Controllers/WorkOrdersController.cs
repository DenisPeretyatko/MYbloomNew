namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class WorkOrdersController : ApiController
    {
        private readonly IWorkOrderService workOrderService;

        public WorkOrdersController(IWorkOrderService workOrderService)
        {
            this.workOrderService = workOrderService;
        }

        public IEnumerable<SageWorkOrder> Get()
        {
            var result = workOrderService.Get();
            return result;
        }

        public SageWorkOrder Get(string id)
        {
            return workOrderService.Get(id);
        }

        public IEnumerable<SageWorkOrder> Post(SagePropertyDictionary properties)
        {
            return workOrderService.Add(properties);
        }

        public IEnumerable<SageWorkOrder> Put(SagePropertyDictionary properties)
        {
            return workOrderService.Edit(properties);
        }
    }
}