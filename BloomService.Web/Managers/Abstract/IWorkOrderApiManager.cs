namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    public interface IWorkOrderApiManager : IEntityApiManager<SageWorkOrder>
    {
        SageResponse<SageWorkOrder> Add( SageWorkOrder workOrder);
    }
}