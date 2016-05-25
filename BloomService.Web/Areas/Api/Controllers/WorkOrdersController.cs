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
            return workOrderService.Get();
        }

        public SageWorkOrder Get(string id)
        {
            return workOrderService.Get(id);
        }

        public IEnumerable<SageWorkOrder> Post(Dictionary<string, string> properties)
        {
            return workOrderService.Add(properties);
        }

        public IEnumerable<SageWorkOrder> Put(Dictionary<string, string> properties)
        {
            return workOrderService.Edit(properties);
        }
    }
}