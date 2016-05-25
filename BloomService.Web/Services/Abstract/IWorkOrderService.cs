namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface IWorkOrderService : IEntityService<SageWorkOrder>
    {
        SageWorkOrder Add(SageWorkOrder workOrder);
    }
}