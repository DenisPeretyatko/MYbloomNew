namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using System.Collections.Generic;
    public interface IWorkOrderApiManager : IEntityApiManager<SageWorkOrder>
    {
        SageResponse<SageWorkOrder> Add( SageWorkOrder workOrder);

        SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties);
    }
}