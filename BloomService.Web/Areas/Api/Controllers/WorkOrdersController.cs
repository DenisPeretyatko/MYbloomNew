namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Linq;
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

        public IHttpActionResult Get()
        {
            var result = workOrderService.Get();
            if (result.Any())
            {
                return Ok(result);
        }

            return NotFound();
        }

        public IHttpActionResult Get(string id)
        {
            var result = workOrderService.Get(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public IHttpActionResult Post(SageWorkOrder workOrder)
        {
            var result = workOrderService.Add(workOrder);
            if (result.IsSucceed)
        {
                return Ok(result.Entity);
            }

            return NotFound();
        }

        public IHttpActionResult Put(SageWorkOrder workOrder)
        {
            var result = workOrderService.Edit(workOrder);
            if (result.IsSucceed)
        {
                return Ok();
            }

            return NotFound();
        }
    }
}