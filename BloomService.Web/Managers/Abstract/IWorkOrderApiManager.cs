namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    public interface IWorkOrderApiManager : IEntityApiManager<SageWorkOrder>
    {
        //SageWorkOrder Add(string endPoint, SageWorkOrder workOrder);

        SageResponse Add(string endPoint, SageWorkOrder workOrder);
    }
}